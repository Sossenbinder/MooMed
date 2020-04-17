using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Net.Client;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.IPC.EndpointResolution.Interface;
using ProtoBuf.Grpc.Client;

namespace MooMed.IPC.EndpointResolution
{
	/// <summary>
	/// Provides Grpc Channels for the caller
	/// </summary>
	public class GrpcChannelProvider : IGrpcChannelProvider
	{
		[NotNull]
		private readonly IStatefulCollectionInfoProvider _statefulCollectionInfoProvider;

		[NotNull]
		private readonly Dictionary<StatefulSet, Dictionary<int, GrpcChannel>> _grpcChannels;

		public GrpcChannelProvider([NotNull] IStatefulCollectionInfoProvider statefulCollectionInfoProvider)
		{
			GrpcClientFactory.AllowUnencryptedHttp2 = true;

			_statefulCollectionInfoProvider = statefulCollectionInfoProvider;

			_grpcChannels = new Dictionary<StatefulSet, Dictionary<int, GrpcChannel>>();
		}

		public async Task<GrpcChannel> GetGrpcChannelForService(StatefulSet statefulSet, int replicaNumber)
		{
			if (_grpcChannels.TryGetValue(statefulSet, out var serviceChannelDict))
			{
				if (serviceChannelDict.ContainsKey(replicaNumber))
				{
					return serviceChannelDict[replicaNumber];
				}
			}

			await RefreshServiceChannels(statefulSet);

			return _grpcChannels[statefulSet][replicaNumber]; 
		}

		private async Task RefreshServiceChannels(StatefulSet statefulSet)
		{
			var statefulCollection = await _statefulCollectionInfoProvider.GetStatefulCollectionInfoForService(statefulSet);

			var channelDict = statefulCollection.StatefulEndpoints.ToDictionary(x => x.InstanceNumber, x => GrpcChannel.ForAddress($"http://{x.IpAddress}:10042"));

			_grpcChannels[statefulSet] = channelDict;
		}
	}
}
