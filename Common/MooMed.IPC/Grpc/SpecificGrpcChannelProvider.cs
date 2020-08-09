using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Grpc.Net.Client;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.IPC.EndpointResolution.Interface;
using MooMed.IPC.Grpc.Interface;

namespace MooMed.IPC.Grpc
{
	internal class SpecificGrpcChannelProvider : ISpecificGrpcChannelProvider
	{
		private const int Port = 10042;

		[NotNull]
		private readonly IEndpointProvider _endpointProvider;

		/// <summary>
		/// Allows to map from stateful service to the cached channels, indexed by their replica number
		/// </summary>
		[NotNull]
		private readonly ConcurrentDictionary<StatefulSetService, ConcurrentDictionary<int, GrpcChannel>> _grpcChannels;

		public SpecificGrpcChannelProvider([NotNull] IEndpointProvider endpointProvider)
		{
			_endpointProvider = endpointProvider;
			_grpcChannels = new ConcurrentDictionary<StatefulSetService, ConcurrentDictionary<int, GrpcChannel>>();
		}

		public GrpcChannel GetGrpcChannel(StatefulSetService moomedService, int replicaNumber = 0)
		{
			if (_grpcChannels.TryGetValue(moomedService, out var serviceChannelDict))
			{
				if (serviceChannelDict.ContainsKey(replicaNumber))
				{
					return serviceChannelDict[replicaNumber];
				}
			}

			_grpcChannels.AddOrUpdate(
				moomedService,
				RefreshServices,
				(key, _) => RefreshServices(key));

			return _grpcChannels[moomedService][replicaNumber];
		}

		private ConcurrentDictionary<int, GrpcChannel> RefreshServices(StatefulSetService moomedService)
		{
			var statefulCollection = _endpointProvider.GetStatefulSetEndpointCollectionInfoForService(moomedService);

			var endpoints = statefulCollection.Endpoints
				.Select(endpoint => new KeyValuePair<int, GrpcChannel>(
					endpoint.InstanceNumber,
					GrpcChannel.ForAddress($"http://{endpoint.IpAddress}:{Port}")));

			return new ConcurrentDictionary<int, GrpcChannel>(endpoints);
		}
	}
}