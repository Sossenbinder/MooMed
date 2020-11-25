using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.DotNet.Utils.Async;
using MooMed.IPC.Grpc.Interface;
using MooMed.ServiceBase.Definitions.Interface;
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
		private readonly ConcurrentDictionary<DeploymentService, AsyncLazy<IGrpcService>> _grpcClientDictionary;

		public GrpcClientProvider([NotNull] IGrpcChannelProvider grpcChannelProvider)
		{
			GrpcClientFactory.AllowUnencryptedHttp2 = true;

			_grpcChannelProvider = grpcChannelProvider;

			_grpcClientDictionary = new ConcurrentDictionary<DeploymentService, AsyncLazy<IGrpcService>>();
		}

		public async ValueTask<TService> GetGrpcClient<TService>(DeploymentService moomedService) where TService : class, IGrpcService
		{
			var grpcService = _grpcClientDictionary.GetOrAdd(
				moomedService,
				key => new AsyncLazy<IGrpcService>(() => CreateNewGrpcService<TService>(key)));

			return await grpcService as TService ?? throw new InvalidOperationException();
		}

		private async Task<IGrpcService> CreateNewGrpcService<TService>(DeploymentService moomedService)
			where TService : class, IGrpcService
		{
			var grpcChannel = await _grpcChannelProvider.GetGrpcChannel(moomedService);

			return grpcChannel.CreateGrpcService<TService>();
		}
	}
}