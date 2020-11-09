using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.IPC.Interface;
using MooMed.IPC.EndpointResolution.Interface;
using MooMed.IPC.Grpc.Interface;
using MooMed.RemotingProxies.ProxyInvocation.Interface;
using MooMed.ServiceBase.Definitions.Interface;

namespace MooMed.RemotingProxies.ProxyInvocation
{
	public abstract class AbstractStatefulSetProxy<TServiceType> : IStatefulSetProxy<TServiceType>
		where TServiceType : class, IGrpcService
	{
		[NotNull]
		private readonly IEndpointProvider _endpointProvider;

		[NotNull]
		private readonly ISpecificGrpcClientProvider _clientProvider;

		[NotNull]
		private readonly IDeterministicPartitionSelectorHelper _deterministicPartitionSelectorHelper;

		[NotNull]
		private readonly Random _random;

		private readonly StatefulSetService _moomedService;

		protected AbstractStatefulSetProxy(
			[NotNull] IEndpointProvider endpointProvider,
			[NotNull] ISpecificGrpcClientProvider clientProvider,
			[NotNull] IDeterministicPartitionSelectorHelper deterministicPartitionSelectorHelper,
			StatefulSetService moomedService)
		{
			_endpointProvider = endpointProvider;
			_clientProvider = clientProvider;
			_deterministicPartitionSelectorHelper = deterministicPartitionSelectorHelper;
			_random = new Random();

			_moomedService = moomedService;
		}

		// Random invocation
		public async Task Invoke(Func<TServiceType, Task> invocationFunc)
		{
			var proxy = await _clientProvider.GetGrpcClient<TServiceType>(
				_moomedService,
				await GetRandomReplicaNumber());

			await invocationFunc(proxy);
		}

		public async Task<TResult> InvokeWithResult<TResult>(Func<TServiceType, Task<TResult>> invocationFunc)
		{
			var proxy = await _clientProvider.GetGrpcClient<TServiceType>(
				_moomedService,
				await GetRandomReplicaNumber());

			return await invocationFunc(proxy);
		}

		// Specific invocation
		public Task InvokeSpecific(
			IEndpointSelector endpointSelector,
			Func<TServiceType, Task> invocationFunc)
			=> InvokeSpecific(endpointSelector.HashableIdentifier, invocationFunc);

		public Task<TResult> InvokeSpecificWithResult<TResult>(
			IEndpointSelector endpointSelector,
			Func<TServiceType, Task<TResult>> invocationFunc)
			=> InvokeSpecificWithResult(endpointSelector.HashableIdentifier, invocationFunc);

		protected Task InvokeSpecific(
			ISessionContextAttachedContainer hashableIdentifier,
			[NotNull] Func<TServiceType, Task> invocationFunc)
			=> InvokeSpecific(hashableIdentifier.SessionContext.HashableIdentifier, invocationFunc);

		protected Task<TResult> InvokeSpecificWithResult<TResult>(
			ISessionContextAttachedContainer hashableIdentifier,
			[NotNull] Func<TServiceType, Task<TResult>> invocationFunc)
			=> InvokeSpecificWithResult(hashableIdentifier.SessionContext.HashableIdentifier, invocationFunc);

		protected async Task InvokeSpecific(
			int hashableIdentifier,
			[NotNull] Func<TServiceType, Task> invocationFunc)
		{
			var proxy = await _clientProvider.GetGrpcClient<TServiceType>(
				_moomedService,
				await ResolveToReplicaNumber(hashableIdentifier));

			await invocationFunc(proxy);
		}

		protected async Task<TResult> InvokeSpecificWithResult<TResult>(
			int hashableIdentifier,
			[NotNull] Func<TServiceType, Task<TResult>> invocationFunc)
		{
			var proxy = await _clientProvider.GetGrpcClient<TServiceType>(
				_moomedService,
				await ResolveToReplicaNumber(hashableIdentifier));

			return await invocationFunc(proxy);
		}

		private async Task<int> ResolveToReplicaNumber(int hashableIdentifier)
		{
			var replicas = await _endpointProvider.GetAvailableReplicasForStatefulService(_moomedService);

			return _deterministicPartitionSelectorHelper.HashIdentifierToPartitionIntIdentifier(hashableIdentifier, replicas);
		}

		private async Task<int> GetRandomReplicaNumber()
		{
			var replicas = await _endpointProvider.GetAvailableReplicasForStatefulService(_moomedService);
			return _random.Next(replicas);
		}
	}
}