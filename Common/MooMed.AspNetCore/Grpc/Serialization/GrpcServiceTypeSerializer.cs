using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MooMed.DotNet.Extensions;
using MooMed.ServiceBase.Definitions.Interface;
using ProtoBuf.Meta;

namespace MooMed.AspNetCore.Grpc.Serialization
{
	/// <summary>
	/// Dynamically registers all parameters and return types of involved
	/// grpc services on the given microservice
	/// </summary>
	public class GrpcServiceTypeSerializer
	{
		private const int PreAllocatedProtoPerService = 100;

		private readonly SerializationHelper _serializationHelper;

		private Dictionary<Type, int> _protoDict;

		public GrpcServiceTypeSerializer(SerializationHelper serializationHelper)
		{
			_serializationHelper = serializationHelper;

			var allGrpcServices = Assembly.GetAssembly(typeof(IGrpcService))
				?.GetTypes()
				.Where(x => x.HasInterface(typeof(IGrpcService)))
				.OrderBy(x => x.FullName)
				.ToList();

			_protoDict = new Dictionary<Type, int>();
			for (var i = 0; i < allGrpcServices.Count; ++i)
			{
				_protoDict.Add(allGrpcServices[i], (i + 1) * PreAllocatedProtoPerService);
			}
		}

		public void SerializeGrpcServices(IEnumerable<IGrpcService> services)
		{
			// Get the types for each registered grpc service. This also includes
			// the type of the currently running service
			var grpcServicesTypes = services
				.Select(x =>
				{
					var interfaces = x
						.GetType()
						.GetInterfaces()
						.ToList();

					return interfaces.Any() ? interfaces.SingleOrDefault(x => x.FullName.StartsWith("MooMed.ServiceBase.Services.Interface")) : null;
				})
				.Where(x => x != null)
				.ToList();

			foreach (var grpcServiceType in grpcServicesTypes)
			{
				InitializeBindingsForGrpcService(grpcServiceType);
			}
		}

		private void InitializeBindingsForGrpcService(Type grpcServiceType)
		{
			var serviceMethods = grpcServiceType.GetMethods();

			// Get all the grpc methods -> All of these are exposed because we are
			// working on the interface anyways
			foreach (var method in serviceMethods)
			{
				var involvedTypes = method
					.GetInvolvedTypesUnWrapped()
					.ToList();

				// Start by registering the generic type chains
				RegisterGenericTypeChain(involvedTypes.Where(x => x.IsGenericType), grpcServiceType);

				RegisterPlainTypes(involvedTypes.Where(x => !x.IsGenericType));
			}
		}

		/// <summary>
		/// Register all generic types on a service method declaration. E.g.
		/// chains ServiceResponse<int> to the respective base chain
		/// </summary>
		/// <param name="genericTypes"></param>
		private void RegisterGenericTypeChain(IEnumerable<Type> genericTypes, Type grpcServiceType)
		{
			foreach (var genericType in genericTypes)
			{
				_serializationHelper.RegisterTypeBaseChain(GetAndIncrementIndex(grpcServiceType), genericType);
			}
		}

		private void RegisterPlainTypes(IEnumerable<Type> plainTypes)
		{
			foreach (var type in plainTypes)
			{
				if (!type.Namespace?.StartsWith("System") ?? false)
				{
					RuntimeTypeModel.Default.Add(type);
				}
			}
		}

		private int GetAndIncrementIndex(Type type)
		{
			var index = _protoDict[type];

			_protoDict[type] = index + 1;

			return index;
		}
	}
}