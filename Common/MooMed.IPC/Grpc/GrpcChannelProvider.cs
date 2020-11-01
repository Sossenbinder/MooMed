using System.Collections.Concurrent;
using System.Threading.Tasks;
using Grpc.Net.Client;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.DotNet.Utils.Async;
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
		private readonly ConcurrentDictionary<DeploymentService, AsyncLazy<GrpcChannel>> _deploymentGrpcChannels;

		public GrpcChannelProvider([NotNull] IEndpointProvider endpointProvider)
		{
			GrpcClientFactory.AllowUnencryptedHttp2 = true;

			_endpointProvider = endpointProvider;

			_deploymentGrpcChannels = new ConcurrentDictionary<DeploymentService, AsyncLazy<GrpcChannel>>();
		}

		public async ValueTask<GrpcChannel> GetGrpcChannel(DeploymentService moomedService)
		{
			var grpcChannel = _deploymentGrpcChannels.GetOrAdd(
				moomedService,
				key => new AsyncLazy<GrpcChannel>(() => RefreshService(key)));

			return await grpcChannel;
		}

		private async Task<GrpcChannel> RefreshService(DeploymentService moomedService)
		{
			var deploymentEndpoint = await _endpointProvider.GetDeploymentEndpoint(moomedService);

			return GrpcChannel.ForAddress($"http://{deploymentEndpoint.DnsName}:{Port}");
		}
	}
}