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
	    private readonly IGrpcChannelProvider _grpcChannelProvider;

        [NotNull]
        private readonly ConcurrentDictionary<StatefulSet, ConcurrentDictionary<int, Task<IGrpcService>>> _grpcClientDictionary;

        public GrpcClientProvider([NotNull] IGrpcChannelProvider grpcChannelProvider)
        {
	        GrpcClientFactory.AllowUnencryptedHttp2 = true;

            _grpcChannelProvider = grpcChannelProvider;

	        _grpcClientDictionary = new ConcurrentDictionary<StatefulSet, ConcurrentDictionary<int, Task<IGrpcService>>>();
        }

        public async Task<TService> GetGrpcClientAsync<TService>(StatefulSet statefulSet, int replicaNr)
	        where TService : class, IGrpcService
		{
	        var grpcServiceDict = _grpcClientDictionary.GetOrAdd(statefulSet, service => new ConcurrentDictionary<int, Task<IGrpcService>>());

	        var grpcService = await grpcServiceDict.GetOrAdd(replicaNr, newReplicaNr => CreateNewGrpcService<TService>(statefulSet, newReplicaNr));

	        return grpcService as TService;
        }

        private async Task<IGrpcService> CreateNewGrpcService<TService>(StatefulSet statefulSet, int channelNumber)
			where TService : class, IGrpcService
        {
	        var grpcChannel = await _grpcChannelProvider.GetGrpcChannelForService(statefulSet, channelNumber);

	        return grpcChannel.CreateGrpcService<TService>();
        }
    }
}
