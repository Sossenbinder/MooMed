using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
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
		private readonly IStatefulCollectionInfoProvider m_statefulCollectionInfoProvider;

		[NotNull]
		private readonly IGrpcClientProvider m_clientProvider;

		[NotNull]
		private readonly IDeterministicPartitionSelectorHelper m_deterministicPartitionSelectorHelper;

		[NotNull]
		private readonly Random m_random;

		private readonly DeployedService m_deployedService;

		protected AbstractProxy(
			[NotNull] IStatefulCollectionInfoProvider statefulCollectionInfoProvider,
			[NotNull] IGrpcClientProvider clientProvider,
			[NotNull] IDeterministicPartitionSelectorHelper deterministicPartitionSelectorHelper,
			DeployedService deployedService)
		{
			m_statefulCollectionInfoProvider = statefulCollectionInfoProvider;
			m_clientProvider = clientProvider;
			m_deterministicPartitionSelectorHelper = deterministicPartitionSelectorHelper;
			m_random = new Random();

			m_deployedService = deployedService;
		}

		public async Task Invoke(Func<TServiceType, Task> invocationFunc)
		{
			var proxy = await m_clientProvider.GetGrpcClientAsync<TServiceType>(
				m_deployedService,
				await GetRandomReplicaNumber());

			await invocationFunc(proxy);
		}

		public async Task<TResult> Invoke<TResult>(Func<TServiceType, Task<TResult>> invocationFunc)
		{
			var proxy = await m_clientProvider.GetGrpcClientAsync<TServiceType>(
				m_deployedService,
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
			var proxy = await m_clientProvider.GetGrpcClientAsync<TServiceType>(
				m_deployedService,
				await ResolveToReplicaNumber(hashableIdentifier));

			 await invocationFunc(proxy);
		}

		protected async Task<TResult> Invoke<TResult>(
			int hashableIdentifier,
			[NotNull] Func<TServiceType, Task<TResult>> invocationFunc)
		{
			var proxy = await m_clientProvider.GetGrpcClientAsync<TServiceType>(
				m_deployedService,
				await ResolveToReplicaNumber(hashableIdentifier));

			return await invocationFunc(proxy);
		}

		[NotNull]
		private Task<int> ResolveToPartitionKey([NotNull] IEndpointSelector sessionContext)
			=> ResolveToReplicaNumber(sessionContext.HashableIdentifier);

		private async Task<int> ResolveToReplicaNumber(int hashableIdentifier)
		{
			var replicas = await m_statefulCollectionInfoProvider.GetAvailableReplicasForService(m_deployedService);

			return m_deterministicPartitionSelectorHelper.HashIdentifierToPartitionIntIdentifier(hashableIdentifier, replicas);
		}

		private async Task<int> GetRandomReplicaNumber()
		{
			var replicas = await m_statefulCollectionInfoProvider.GetAvailableReplicasForService(m_deployedService);
			return m_random.Next(replicas);
		}
	}
}
