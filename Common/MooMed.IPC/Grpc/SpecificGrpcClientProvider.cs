using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.DotNet.Utils.Async;
using MooMed.IPC.Grpc.Interface;
using MooMed.ServiceBase.Definitions.Interface;
using ProtoBuf.Grpc.Client;

namespace MooMed.IPC.Grpc
{
	internal class SpecificGrpcClientProvider : ISpecificGrpcClientProvider
	{
		[NotNull]
		private readonly ISpecificGrpcChannelProvider _grpcChannelProvider;

		[NotNull]
		private readonly ConcurrentDictionary<StatefulSetService, ConcurrentDictionary<int, AsyncLazy<IGrpcService>>> _grpcClientDictionary;

		public SpecificGrpcClientProvider([NotNull] ISpecificGrpcChannelProvider grpcChannelProvider)
		{
			GrpcClientFactory.AllowUnencryptedHttp2 = true;

			_grpcChannelProvider = grpcChannelProvider;

			_grpcClientDictionary = new ConcurrentDictionary<StatefulSetService, ConcurrentDictionary<int, AsyncLazy<IGrpcService>>>();
		}

		public async ValueTask<TService> GetGrpcClient<TService>(StatefulSetService moomedService, int replicaNumber = 0) where TService : class, IGrpcService
		{
			var grpcServiceDict = _grpcClientDictionary.GetOrAdd(
				moomedService,
				service => new ConcurrentDictionary<int, AsyncLazy<IGrpcService>>());

			var grpcService = await grpcServiceDict.GetOrAdd(
				replicaNumber,
				newReplicaNr => new AsyncLazy<IGrpcService>(() => CreateNewGrpcService<TService>(moomedService, newReplicaNr)));

			return grpcService as TService ?? throw new InvalidOperationException();
		}

		private async Task<IGrpcService> CreateNewGrpcService<TService>(StatefulSetService moomedService, int channelNumber)
			where TService : class, IGrpcService
		{
			var grpcChannel = await _grpcChannelProvider.GetGrpcChannel(moomedService, channelNumber);

			return grpcChannel.CreateGrpcService<TService>();
		}
	}
}