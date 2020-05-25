using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.Chat;
using MooMed.Common.ServiceBase;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Core.Code.Extensions;
using MooMed.Core.DataTypes;
using MooMed.Grpc.Definitions.Interface;
using ProtoBuf.Meta;

namespace MooMed.AspNetCore.Grpc
{
	public class SerializationModelBinderService : IStartable
	{
		[NotNull]
		private readonly IEnumerable<Type> _grpcServices;

		private readonly Dictionary<Type, int> _protoIndexDict;

		private int _protoIndex = 50;

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

			_grpcServices = grpcServices.Select(service =>
				service.GetInterfaces().First(i =>
					i.FullName != null && i.FullName.StartsWith("MooMed.Common.ServiceBase.Interface")));

			var allGrpcServices = Assembly
				.GetAssembly(typeof(MooMedServiceBase))
				.GetTypes()
				.Where(x => x.GetInterface("IGrpcService") != null)
				.Select(x => x);

			var baseProtoIndex = 50;
			
			_protoIndexDict = allGrpcServices.ToDictionary(x => x, x => baseProtoIndex += 50);
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

				var genericTypes = cleanTypes.Where(x => x.IsGenericType);
				
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
				return _protoIndex++;
			}

			var index = _protoIndexDict[type];

			_protoIndexDict[type] = index + 1;

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

		private void BindSessionContextAttachedContainers()
		{
			var containerType = typeof(SessionContextAttachedContainer);
			var index = 700;

			var containerImplementers = Assembly
				.GetAssembly(containerType)
				?.GetTypes()
				.Where(x => x.BaseType == containerType).ToList();

			if (containerImplementers.IsNullOrEmpty())
			{
				return;
			}

			var baseMetaData = RuntimeTypeModel.Default.Add(containerType);

			foreach (var implementer in containerImplementers)
			{
				baseMetaData.AddSubType(index, implementer);
				index++;
			}
		}

		public void Start()
		{
			foreach (var grpcService in _grpcServices)
			{
				InitializeBindingsForGrpcService(grpcService);
			}

			BindSessionContextAttachedContainers();
		}
	}
}
