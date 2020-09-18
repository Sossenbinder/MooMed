﻿using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GrpcProxyGenerator.DataTypes;
using GrpcProxyGenerator.Extensions;
using GrpcProxyGenerator.Service.InternalGenerators.Interface;
using MooMed.Common.Definitions.IPC;
using MooMed.DotNet.Extensions;
using MooMed.IPC.Grpc.Interface;
using MooMed.IPC.ProxyInvocation;
using MooMed.ServiceBase.Definitions.Interface;

namespace GrpcProxyGenerator.Service.InternalGenerators
{
	internal class DeploymentProxyInternalsGenerator : IProxyInternalsGenerator
	{
		// Needed for nameof() with non-static object methods
		private AbstractDeploymentProxy<IGrpcService> _proxyObj;

		public void GenerateProxyInternals(StringBuilder stringBuilder, ProxyMetaData metaData)
		{
			GenerateConstructor(stringBuilder, metaData);
			GenerateMethods(stringBuilder, metaData);
		}

		private void GenerateConstructor(StringBuilder stringBuilder, ProxyMetaData metaData)
		{
			stringBuilder.Tab(2).AppendLine($"public {metaData.ServiceNameShort}Proxy({nameof(IGrpcClientProvider)} clientProvider)");
			stringBuilder.Tab(3).AppendLine(": base(clientProvider,");
			stringBuilder.Tab(4).AppendLine($"{nameof(DeploymentService)}.{metaData.ServiceNameShort})");
			stringBuilder.Tab(2).AppendLine("{ }");
		}

		private void GenerateMethods(StringBuilder stringBuilder, ProxyMetaData metaData)
		{
			var methods = metaData.Type.GetMethods();

			foreach (var methodInfo in methods)
			{
				AddMethod(stringBuilder, methodInfo);
			}
		}

		private void AddMethod(StringBuilder stringBuilder, MethodInfo methodInfo)
		{
			// Needs to be cleaned, as this will always be a Task when running over grpc
			var returnType = methodInfo.ReturnType.GetRealFullName();

			var methodName = methodInfo.Name;

			var parameters = methodInfo.GetParameters();

			// Add common method visibility, return type and name
			stringBuilder.Tab(2).Append($"public {returnType} {methodName}");

			// Add parameters
			if (parameters.Length <= 1)
			{
				var parameter = parameters.Single();
				stringBuilder.AppendLine($"({parameter.ParameterType.GetRealName()} {StripGenericArtifacts(parameter.Name)})");
			}
			else
			{
				stringBuilder.AppendLine("(");

				for (var i = 0; i < parameters.Length; ++i)
				{
					var parameter = parameters[i];
					stringBuilder.Tab(3).Append($"{parameter.ParameterType.GetRealName()} {StripGenericArtifacts(parameter.Name)}");
					stringBuilder.AppendLine(i == parameters.Length - 1 ? ")" : ",");
				}
			}

			// Add method forward call
			stringBuilder.Tab(3).Append($"=> {(methodInfo.ReturnType == typeof(Task) ? nameof(_proxyObj.Invoke) : nameof(_proxyObj.InvokeWithResult))}");
			stringBuilder.Append($"(service => service.{methodName}(");

			if (parameters.Length <= 1)
			{
				var parameter = parameters.Single();
				stringBuilder.AppendLine($"({StripGenericArtifacts(parameter.Name)});");
			}
			else
			{
				stringBuilder.AppendLine();
				for (var i = 0; i < parameters.Length; i++)
				{
					var parameter = parameters[i];
					stringBuilder.Tab(4).Append(StripGenericArtifacts(parameter.Name));
					stringBuilder.AppendLine(i == parameters.Length - 1 ? ");" : ",");
				}
			}

			stringBuilder.AppendLine();
		}

		private static string StripGenericArtifacts(string? name)
		{
			if (name == null)
			{
				return "";
			}

			var genericOpen = name.IndexOf('[');
			if (genericOpen != -1)
			{
				name = name.Replace('[', '<');
				name = name.Replace('[', '>');
			}

			var compilerGeneratedSuffix = name.IndexOf('`');
			if (compilerGeneratedSuffix != -1)
			{
				name = name[..compilerGeneratedSuffix];
			}

			return name;
		}
	}
}