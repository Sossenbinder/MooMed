using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.ServiceBase;
using MooMed.Core.Code.Extensions;
using MooMed.Core.DataTypes;
using MooMed.Grpc.Definitions.Interface;
using ProtoBuf.Meta;

namespace MooMed.AspNetCore.Grpc
{
	public class SerializationModelBinderService : IStartable
	{
		[NotNull]
		private readonly IEnumerable<Type> m_grpServices;

		private readonly Dictionary<Type, int> m_protoIndexDict;

		private int m_protoIndex = 50;

		public SerializationModelBinderService([NotNull] IEnumerable<IGrpcService> services)
		{
			var grpcServices = services.Select(x => x.GetType()).ToList();

			var ownServiceType = Assembly
				.GetEntryAssembly()
				?.GetTypes()
				.SingleOrDefault(x => x.BaseType == typeof(MooMedServiceBase));

			if (ownServiceType != null)
			{
				grpcServices.Add(ownServiceType);
			}

			m_grpServices = grpcServices.Select(service =>
				service.GetInterfaces().First(i =>
					i.FullName != null && i.FullName.StartsWith("MooMed.Common.ServiceBase.Interface")));

			var allGrpcServices = Assembly
				.GetAssembly(typeof(MooMedServiceBase))
				.GetTypes()
				.Where(x => x.GetInterface("IGrpcService") != null)
				.Select(x => x);

			var baseProtoIndex = 50;

			m_protoIndexDict = allGrpcServices.ToDictionary(x => x, x => baseProtoIndex += 50);
		}

		private void InitializeBindingsForGrpcService([NotNull] Type grpcService)
		{
			foreach (var method in grpcService.GetMethods())
			{
				var involvedTypes = method.GetParameters().Select(x => x.ParameterType).ToList();
				involvedTypes.Add(method.ReturnType);

				var cleanTypes = involvedTypes.Where(x => !x.IsGenericType).ToList();
				var taskCleanTypes = involvedTypes.Except(cleanTypes).Select(x => x.CheckAndGetTaskWrappedType());
				cleanTypes.AddRange(taskCleanTypes);

				var genericTypes = cleanTypes.Where(x =>x.IsGenericType);

				foreach (var genericType in genericTypes)
				{
					RegisterBaseChain(genericType, grpcService);
				}
				
				var nonGenerics = cleanTypes.Where(x => !x.IsGenericType);
				foreach (var type in nonGenerics)
				{
					if (!type.Namespace.StartsWith("System"))
					{
						RuntimeTypeModel.Default.Add(type, true);
					}
				}
			}
		}

		private int GetAndIncrementIndex([CanBeNull] Type type)
		{
			if (type == null)
			{
				return m_protoIndex++;
			}

			var index = m_protoIndexDict[type];

			m_protoIndexDict[type] = index + 1;

			return index;
		}

		private void RegisterBaseChain([NotNull] Type type, [NotNull] Type serviceType)
		{
			var baseType = type.BaseType;

			if (baseType == null || baseType == typeof(object))
			{
				return;
			}

			var baseMetaData = RuntimeTypeModel.Default.Add(baseType);

			baseMetaData.AddSubType(GetAndIncrementIndex(serviceType), type);

			RegisterBaseChain(baseType, serviceType);
		}

		public void Start()
		{
			foreach (var grpcService in m_grpServices)
			{
				InitializeBindingsForGrpcService(grpcService);
			}

			var entryAssembly = Assembly.GetEntryAssembly()?.FullName;
			var types = RuntimeTypeModel.Default.GetTypes();
		}
	}
}
