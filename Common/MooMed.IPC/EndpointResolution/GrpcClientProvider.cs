using System.Collections.Concurrent;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Grpc.Definitions.Interface;
using MooMed.IPC.EndpointResolution.Interface;
using ProtoBuf.Grpc.Client;

namespace MooMed.IPC.EndpointResolution
{
    /// <summary>
    /// Provides Grpc Clients
    /// </summary>
    public class GrpcClientProvider : IGrpcClientProvider
    {
		[NotNull]
	    private readonly IGrpcChannelProvider m_grpcChannelProvider;

        [NotNull]
        private readonly ConcurrentDictionary<DeployedService, ConcurrentDictionary<int, Task<IGrpcService>>> m_grpcClientDictionary;

        public GrpcClientProvider([NotNull] IGrpcChannelProvider grpcChannelProvider)
        {
	        m_grpcChannelProvider = grpcChannelProvider;

	        m_grpcClientDictionary = new ConcurrentDictionary<DeployedService, ConcurrentDictionary<int, Task<IGrpcService>>>();
        }

        public async Task<TService> GetGrpcClientAsync<TService>(DeployedService deployedService, int channelNumber)
	        where TService : class, IGrpcService
		{
	        var grpcServiceDict = m_grpcClientDictionary.GetOrAdd(deployedService, service => new ConcurrentDictionary<int, Task<IGrpcService>>());

	        var grpcService = await grpcServiceDict.GetOrAdd(channelNumber, channelNr => CreateNewGrpcService<TService>(deployedService, channelNr));

	        return grpcService as TService;
        }

        private async Task<IGrpcService> CreateNewGrpcService<TService>(DeployedService deployedService, int channelNumber)
			where TService : class, IGrpcService
        {
	        var grpcChannel = await m_grpcChannelProvider.GetGrpcChannelForService(deployedService, channelNumber);

	        return grpcChannel.CreateGrpcService<TService>();
        }
    }
}
