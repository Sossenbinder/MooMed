using System.Collections.Concurrent;
using Grpc.Net.Client;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.IPC.EndpointResolution.Interface;
using MooMed.IPC.Grpc.Interface;
using ProtoBuf.Grpc.Client;

namespace MooMed.IPC.Grpc
{
	/// <summary>
	/// Provides Grpc Channels for the caller
	/// </summary>
	public class GrpcChannelProvider : IGrpcChannelProvider
	{
		private const int Port = 10042;

		[NotNull]
		private readonly IEndpointProvider _endpointProvider;

		[NotNull]
		private readonly ConcurrentDictionary<DeploymentService, GrpcChannel> _deploymentGrpcChannels;

		public GrpcChannelProvider([NotNull] IEndpointProvider endpointProvider)
		{
			GrpcClientFactory.AllowUnencryptedHttp2 = true;

			_endpointProvider = endpointProvider;

			_deploymentGrpcChannels = new ConcurrentDictionary<DeploymentService, GrpcChannel>();
		}

		public GrpcChannel GetGrpcChannel(DeploymentService moomedService)
		{
			var grpcChannel = _deploymentGrpcChannels.GetOrAdd(
				moomedService,
				RefreshService);

			return grpcChannel;
		}

		private GrpcChannel RefreshService(DeploymentService moomedService)
		{
			var deploymentEndpoint = _endpointProvider.GetDeploymentEndpoint(moomedService);

			return GrpcChannel.ForAddress($"http://{deploymentEndpoint.IpAddress}:{Port}");
		}
	}
}