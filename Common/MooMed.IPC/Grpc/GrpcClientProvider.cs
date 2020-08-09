using System;
using System.Collections.Concurrent;
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
		private readonly ConcurrentDictionary<DeploymentService, IGrpcService> _grpcClientDictionary;

		public GrpcClientProvider([NotNull] IGrpcChannelProvider grpcChannelProvider)
		{
			GrpcClientFactory.AllowUnencryptedHttp2 = true;

			_grpcChannelProvider = grpcChannelProvider;

			_grpcClientDictionary = new ConcurrentDictionary<DeploymentService, IGrpcService>();
		}

		public TService GetGrpcClient<TService>(DeploymentService moomedService) where TService : class, IGrpcService
		{
			var grpcService = _grpcClientDictionary.GetOrAdd(
				moomedService,
				CreateNewGrpcService<TService>);

			return grpcService as TService ?? throw new InvalidOperationException();
		}

		private IGrpcService CreateNewGrpcService<TService>(DeploymentService moomedService)
			where TService : class, IGrpcService
		{
			var grpcChannel = _grpcChannelProvider.GetGrpcChannel(moomedService);

			return grpcChannel.CreateGrpcService<TService>();
		}
	}
}