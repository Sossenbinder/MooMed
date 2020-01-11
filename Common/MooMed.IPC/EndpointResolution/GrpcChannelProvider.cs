using System.Collections.Generic;
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
		private readonly IStatefulCollectionInfoProvider m_statefulCollectionInfoProvider;

		[NotNull]
		private readonly Dictionary<DeployedService, Dictionary<int, GrpcChannel>> m_grpcChannels;

		public GrpcChannelProvider([NotNull] IStatefulCollectionInfoProvider statefulCollectionInfoProvider)
		{
			GrpcClientFactory.AllowUnencryptedHttp2 = true;

			m_statefulCollectionInfoProvider = statefulCollectionInfoProvider;

			m_grpcChannels = new Dictionary<DeployedService, Dictionary<int, GrpcChannel>>();
		}

		public async Task<GrpcChannel> GetGrpcChannelForService(DeployedService deployedService, int channelNumber)
		{
			if (m_grpcChannels.TryGetValue(deployedService, out var serviceChannelDict))
			{
				if (serviceChannelDict.ContainsKey(channelNumber))
				{
					return serviceChannelDict[channelNumber];
				}
			}

			await RefreshServiceChannels(deployedService);

			return m_grpcChannels[deployedService][channelNumber]; 
		}

		private async Task RefreshServiceChannels(DeployedService deployedService)
		{
			var statefulCollection = await m_statefulCollectionInfoProvider.GetStatefulCollectionInfoForService(deployedService);

			var channelDict = new Dictionary<int, GrpcChannel>();

			foreach (var pod in statefulCollection.StatefulEndpoints)
			{
				channelDict.Add(pod.InstanceNumber, GrpcChannel.ForAddress($"http://{pod.IpAddress}:10042"));
			}

			m_grpcChannels[deployedService] = channelDict;
		}
	}
}
