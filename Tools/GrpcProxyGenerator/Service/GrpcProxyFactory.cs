using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using GrpcProxyGenerator.DataTypes;
using GrpcProxyGenerator.Extensions;
using GrpcProxyGenerator.Service.Interface;
using GrpcProxyGenerator.Service.InternalGenerators;
using GrpcProxyGenerator.Service.InternalGenerators.Interface;
using MooMed.Common.Definitions.IPC;
using MooMed.DotNet.Extensions;
using MooMed.ServiceBase.Attributes;

namespace GrpcProxyGenerator.Service
{
	internal class GrpcProxyFactory : IGrpcProxyFactory
	{
		private readonly IGrpcProxyEmitter _grpcProxyEmitter;

		private readonly ITypeInfoProvider _typeInfoProvider;

		private static readonly Dictionary<ServiceType, IProxyInternalsGenerator> ProxyInternalsGenerators = new Dictionary<ServiceType, IProxyInternalsGenerator>()
		{
			{ServiceType.Deployment, new DeploymentProxyInternalsGenerator()},
			{ServiceType.StatefulSet, new StatefulSetProxyInternalsGenerator()},
		};

		public GrpcProxyFactory(
			IGrpcProxyEmitter grpcProxyEmitter,
			ITypeInfoProvider typeInfoProvider)
		{
			_grpcProxyEmitter = grpcProxyEmitter;
			_typeInfoProvider = typeInfoProvider;
		}

		public void GenerateProxy(Type proxyInterface)
		{
			var proxyMetaData = _typeInfoProvider.GetMetaData(proxyInterface);

			var proxyCodeBuilder = new StringBuilder();

			proxyCodeBuilder.AppendLine($"namespace MooMed.{proxyMetaData.ServiceNameShort}.Remoting");
			proxyCodeBuilder.AppendLine("{");
			proxyCodeBuilder.Tab().AppendLine($"public class {proxyMetaData.ServiceNameShort}Proxy : {GetKubernetesType(proxyMetaData)}<{proxyMetaData.Type.FullName}>, {proxyMetaData.Type.FullName}").LineBreak();
			proxyCodeBuilder.Tab().AppendLine("{");

			GenerateClassInternals(proxyCodeBuilder, proxyMetaData);

			proxyCodeBuilder.Tab().AppendLine("}");
			proxyCodeBuilder.AppendLine("}");

			var builtProxy = proxyCodeBuilder.ToString();

			_grpcProxyEmitter.EmitProxy(builtProxy, proxyMetaData);
		}

		private static void GenerateClassInternals(StringBuilder proxyCodeBuilder, ProxyMetaData proxyMetaData)
		{
			proxyCodeBuilder.Tab(2).AppendLine();

			ProxyInternalsGenerators[proxyMetaData.KubernetesServiceType].GenerateProxyInternals(proxyCodeBuilder, proxyMetaData);
		}

		private string GetKubernetesType(ProxyMetaData proxyMetaData)
		{
			return _typeInfoProvider.GetProxyTypeForType(proxyMetaData.KubernetesServiceType).GetRealFullName();
		}
	}
}