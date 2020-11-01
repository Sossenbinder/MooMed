using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Net.Client;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.DotNet.Utils.Async;
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
		private readonly ConcurrentDictionary<StatefulSetService, AsyncLazy<ConcurrentDictionary<int, GrpcChannel>>> _grpcChannels;

		public SpecificGrpcChannelProvider([NotNull] IEndpointProvider endpointProvider)
		{
			_endpointProvider = endpointProvider;
			_grpcChannels = new ConcurrentDictionary<StatefulSetService, AsyncLazy<ConcurrentDictionary<int, GrpcChannel>>>();
		}

		public async ValueTask<GrpcChannel> GetGrpcChannel(StatefulSetService moomedService, int replicaNumber = 0)
		{
			if (_grpcChannels.TryGetValue(moomedService, out var serviceChannelDict))
			{
				var serviceChannel = await serviceChannelDict;
				if (serviceChannel.ContainsKey(replicaNumber))
				{
					return serviceChannel[replicaNumber];
				}
			}

			await _grpcChannels.AddOrUpdate(
				moomedService,
				key => new AsyncLazy<ConcurrentDictionary<int, GrpcChannel>>(() => RefreshServices(key)),
				(key, _) => new AsyncLazy<ConcurrentDictionary<int, GrpcChannel>>(() => RefreshServices(key)));

			return (await _grpcChannels[moomedService])[replicaNumber];
		}

		private async Task<ConcurrentDictionary<int, GrpcChannel>> RefreshServices(StatefulSetService moomedService)
		{
			var statefulCollection = await _endpointProvider.GetStatefulSetEndpointCollectionInfoForService(moomedService);

			var endpoints = statefulCollection.Endpoints
				.Select(endpoint => new KeyValuePair<int, GrpcChannel>(
					endpoint.InstanceNumber,
					GrpcChannel.ForAddress($"http://{endpoint.DnsName}:{Port}")));

			return new ConcurrentDictionary<int, GrpcChannel>(endpoints);
		}
	}
}