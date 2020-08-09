using System;
using System.Collections.Concurrent;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Grpc.Definitions.Interface;
using MooMed.IPC.Grpc.Interface;
using ProtoBuf.Grpc.Client;

namespace MooMed.IPC.Grpc
{
	internal class SpecificGrpcClientProvider : ISpecificGrpcClientProvider
	{
		[NotNull]
		private readonly ISpecificGrpcChannelProvider _grpcChannelProvider;

		[NotNull]
		private readonly ConcurrentDictionary<StatefulSetService, ConcurrentDictionary<int, IGrpcService>> _grpcClientDictionary;

		public SpecificGrpcClientProvider([NotNull] ISpecificGrpcChannelProvider grpcChannelProvider)
		{
			GrpcClientFactory.AllowUnencryptedHttp2 = true;

			_grpcChannelProvider = grpcChannelProvider;

			_grpcClientDictionary = new ConcurrentDictionary<StatefulSetService, ConcurrentDictionary<int, IGrpcService>>();
		}

		public TService GetGrpcClient<TService>(StatefulSetService moomedService, int replicaNumber = 0) where TService : class, IGrpcService
		{
			var grpcServiceDict = _grpcClientDictionary.GetOrAdd(
				moomedService,
				service => new ConcurrentDictionary<int, IGrpcService>());

			var grpcService = grpcServiceDict.GetOrAdd(
				replicaNumber,
				newReplicaNr => CreateNewGrpcService<TService>(moomedService, newReplicaNr));

			return grpcService as TService ?? throw new InvalidOperationException();
		}

		private IGrpcService CreateNewGrpcService<TService>(StatefulSetService moomedService, int channelNumber)
			where TService : class, IGrpcService
		{
			var grpcChannel = _grpcChannelProvider.GetGrpcChannel(moomedService, channelNumber);

			return grpcChannel.CreateGrpcService<TService>();
		}
	}
}