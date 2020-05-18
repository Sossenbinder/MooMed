using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Grpc.Definitions.Interface;
using MooMed.IPC.Grpc.Interface;
using ProtoBuf.Grpc.Client;

namespace MooMed.IPC.Grpc
{
    /// <summary>
    /// Provides Grpc Clients
    /// </summary>
    public class GrpcClientProvider : IGrpcClientProvider
    {
		[NotNull]
	    private readonly IGrpcChannelProvider _grpcChannelProvider;

        [NotNull]
        private readonly ConcurrentDictionary<MooMedService, ConcurrentDictionary<int, IGrpcService>> _grpcClientDictionary;

        public GrpcClientProvider([NotNull] IGrpcChannelProvider grpcChannelProvider)
        {
	        GrpcClientFactory.AllowUnencryptedHttp2 = true;

            _grpcChannelProvider = grpcChannelProvider;

	        _grpcClientDictionary = new ConcurrentDictionary<MooMedService, ConcurrentDictionary<int, IGrpcService>>();
        }
        
        public TService GetGrpcClient<TService>(MooMedService moomedService, int replicaNumber = 0) where TService : class, IGrpcService
        {
	        var grpcServiceDict = _grpcClientDictionary.GetOrAdd(moomedService, 
		        service => new ConcurrentDictionary<int, IGrpcService>());

	        var grpcService = grpcServiceDict.GetOrAdd(replicaNumber, 
		        newReplicaNr => CreateNewGrpcService<TService>(moomedService, newReplicaNr));

	        return grpcService as TService ?? throw new InvalidOperationException();
        }

        private IGrpcService CreateNewGrpcService<TService>(MooMedService moomedService, int channelNumber)
			where TService : class, IGrpcService
        {
	        var grpcChannel = _grpcChannelProvider.GetGrpcChannel(moomedService, channelNumber);

	        return grpcChannel.CreateGrpcService<TService>();
        }
    }
}
