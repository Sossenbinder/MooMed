using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Autofac;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.ServiceBase;
using MooMed.DotNet.Extensions;
using MooMed.Grpc.Definitions.Interface;
using MooMed.Logging.Loggers.Interface;
using ProtoBuf.Meta;

namespace MooMed.AspNetCore.Grpc
{
	public class SerializationModelBinderService : IStartable
	{
		private readonly IMooMedLogger _logger;

		private readonly IEnumerable<IGrpcService> _services;

		private int _protoIndex = 50;

		public SerializationModelBinderService(
			IMooMedLogger logger,
			IEnumerable<IGrpcService> services)
		{
			_logger = logger;
			_services = services;
		}

		private void InitializeBindingsForGrpcService(Dictionary<Type, int> protoIndexDict, Type grpcService)
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
					RegisterBaseChain(protoIndexDict, genericType, grpcService);
				}
				
				var nonGenerics = cleanTypes.Where(x => !x.IsGenericType);
				foreach (var type in nonGenerics)
				{
					if (!type.Namespace?.StartsWith("System") ?? false)
					{
						RuntimeTypeModel.Default.Add(type);
					}
				}
			}
		}

		private void RegisterBaseChain(Dictionary<Type, int> protoIndexDict, Type type, Type serviceType)
		{
			while (true)
			{
				var baseType = type.BaseType;

				if (baseType == null || baseType == typeof(object))
				{
					return;
				}

				var baseMetaData = RuntimeTypeModel.Default.Add(baseType);

				baseMetaData.AddSubType(GetAndIncrementIndex(protoIndexDict, serviceType), type);

				type = baseType;
			}
		}

		private int GetAndIncrementIndex(Dictionary<Type, int> protoIndexDict, Type type)
		{
			if (type == null)
			{
				return _protoIndex++;
			}

			var index = protoIndexDict[type];

			protoIndexDict[type] = index + 1;

			return index;
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
			var stopWatch = Stopwatch.StartNew();

			var serviceTypes = _services.Select(x => x.GetType()).ToList();

			var ownServiceType = Assembly
				.GetEntryAssembly()
				?.GetTypes()
				.SingleOrDefault(x => x.BaseType == typeof(MooMedServiceBase));

			if (ownServiceType != null)
			{
				serviceTypes.Add(ownServiceType);
			}

			var grpcServices = serviceTypes.Select(service =>
				service.GetInterfaces().First(i =>
					i.FullName != null && i.FullName.StartsWith("MooMed.Common.ServiceBase.Interface")));

			var allGrpcServices = Assembly
				.GetAssembly(typeof(MooMedServiceBase))
				?.GetTypes()
				.Where(x => x.GetInterface("IGrpcService") != null)
				.Select(x => x);

			var baseProtoIndex = 50;

			var protoIndexDict = allGrpcServices?.ToDictionary(x => x, x => baseProtoIndex += 50);

			if (protoIndexDict != null)
			{
				foreach (var grpcService in grpcServices)
				{
					InitializeBindingsForGrpcService(protoIndexDict, grpcService);
				}
			}

			BindSessionContextAttachedContainers();

			stopWatch.Stop();
			_logger.Info($"Initialization of serialization model service took {stopWatch.Elapsed}");
		}
	}
}
