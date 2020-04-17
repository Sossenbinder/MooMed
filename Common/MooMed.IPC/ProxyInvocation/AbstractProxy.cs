using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.IPC.Interface;
using MooMed.Grpc.Definitions.Interface;
using MooMed.IPC.EndpointResolution.Interface;
using MooMed.IPC.ProxyInvocation.Interface;

namespace MooMed.IPC.ProxyInvocation
{
	/// <summary>
	/// Serves as the base for all proxy services, taking care of load balancing for all the implementers
	/// </summary>
	/// <typeparam name="TServiceType"></typeparam>
	public abstract class AbstractProxy<TServiceType> : IProxy<TServiceType> 
		where TServiceType : class, IGrpcService
	{
		[NotNull]
		private readonly IStatefulCollectionInfoProvider _statefulCollectionInfoProvider;

		[NotNull]
		private readonly IGrpcClientProvider _clientProvider;

		[NotNull]
		private readonly IDeterministicPartitionSelectorHelper _deterministicPartitionSelectorHelper;

		[NotNull]
		private readonly Random _random;

		private readonly StatefulSet _statefulSet;

		protected AbstractProxy(
			[NotNull] IStatefulCollectionInfoProvider statefulCollectionInfoProvider,
			[NotNull] IGrpcClientProvider clientProvider,
			[NotNull] IDeterministicPartitionSelectorHelper deterministicPartitionSelectorHelper,
			StatefulSet statefulSet)
		{
			_statefulCollectionInfoProvider = statefulCollectionInfoProvider;
			_clientProvider = clientProvider;
			_deterministicPartitionSelectorHelper = deterministicPartitionSelectorHelper;
			_random = new Random();

			_statefulSet = statefulSet;
		}

		public async Task InvokeOnRandomReplica(Func<TServiceType, Task> invocationFunc)
		{
			var proxy = await _clientProvider.GetGrpcClientAsync<TServiceType>(
				_statefulSet,
				await GetRandomReplicaNumber());

			await invocationFunc(proxy);
		}

		public async Task<TResult> InvokeOnRandomReplica<TResult>(Func<TServiceType, Task<TResult>> invocationFunc)
		{
			var proxy = await _clientProvider.GetGrpcClientAsync<TServiceType>(
				_statefulSet,
				await GetRandomReplicaNumber());

			return await invocationFunc(proxy);
		}

		public Task Invoke(
			IEndpointSelector endpointSelector,
			Func<TServiceType, Task> invocationFunc)
			=> Invoke(endpointSelector.HashableIdentifier, invocationFunc);

		public Task<TResult> Invoke<TResult>(
			IEndpointSelector endpointSelector,
			Func<TServiceType, Task<TResult>> invocationFunc)
			=> Invoke(endpointSelector.HashableIdentifier, invocationFunc);

		protected async Task Invoke(
			int hashableIdentifier,
			[NotNull] Func<TServiceType, Task> invocationFunc)
		{
			var proxy = await _clientProvider.GetGrpcClientAsync<TServiceType>(
				_statefulSet,
				await ResolveToReplicaNumber(hashableIdentifier));

			 await invocationFunc(proxy);
		}

		protected async Task<TResult> Invoke<TResult>(
			int hashableIdentifier,
			[NotNull] Func<TServiceType, Task<TResult>> invocationFunc)
		{
			var proxy = await _clientProvider.GetGrpcClientAsync<TServiceType>(
				_statefulSet,
				await ResolveToReplicaNumber(hashableIdentifier));

			return await invocationFunc(proxy);
		}

		protected async Task Invoke(
			ISessionContextAttachedContainer hashableIdentifier,
			[NotNull] Func<TServiceType, Task> invocationFunc)
		{
			var proxy = await _clientProvider.GetGrpcClientAsync<TServiceType>(
				_statefulSet,
				await ResolveToReplicaNumber(hashableIdentifier.SessionContext.HashableIdentifier));

			await invocationFunc(proxy);
		}

		protected async Task<TResult> Invoke<TResult>(
			ISessionContextAttachedContainer hashableIdentifier,
			[NotNull] Func<TServiceType, Task<TResult>> invocationFunc)
		{
			var proxy = await _clientProvider.GetGrpcClientAsync<TServiceType>(
				_statefulSet,
				await ResolveToReplicaNumber(hashableIdentifier.SessionContext.HashableIdentifier));

			return await invocationFunc(proxy);
		}

		[NotNull]
		private Task<int> ResolveToPartitionKey([NotNull] IEndpointSelector sessionContext)
			=> ResolveToReplicaNumber(sessionContext.HashableIdentifier);

		private async Task<int> ResolveToReplicaNumber(int hashableIdentifier)
		{
			var replicas = await _statefulCollectionInfoProvider.GetAvailableReplicasForService(_statefulSet);

			return _deterministicPartitionSelectorHelper.HashIdentifierToPartitionIntIdentifier(hashableIdentifier, replicas);
		}

		private async Task<int> GetRandomReplicaNumber()
		{
			var replicas = await _statefulCollectionInfoProvider.GetAvailableReplicasForService(_statefulSet);
			return _random.Next(replicas);
		}
	}
}
