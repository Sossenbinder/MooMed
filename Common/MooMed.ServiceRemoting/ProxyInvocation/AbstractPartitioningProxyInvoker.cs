using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.ServiceBase.Service.Interface.Base;
using MooMed.ServiceRemoting.DataType;
using MooMed.ServiceRemoting.EndpointResolution.Interface;
using MooMed.ServiceRemoting.Interface;
using MooMed.ServiceRemoting.ProxyInvocation.Interface;

namespace MooMed.ServiceRemoting.ProxyInvocation
{
	public abstract class AbstractPartitioningProxyInvoker<TServiceType> : IPartitioningProxyInvoker<TServiceType> where TServiceType : IRemotingServiceBase
	{
		[NotNull]
		private readonly IRemotingProxyProvider m_proxyProvider;

		[NotNull]
		private readonly IPartitionInfoProvider m_partitionInfoProvider;

		[NotNull]
		private readonly IDeterministicPartitionSelectorHelper m_deterministicPartitionSelectorHelper;

		[NotNull]
		private readonly Random m_random;

		private readonly DeployedFabricApplication m_deployedFabricApplication;

		private readonly DeployedFabricService m_deployedFabricService;

		protected AbstractPartitioningProxyInvoker(
			[NotNull] IRemotingProxyProvider proxyProvider,
			[NotNull] IPartitionInfoProvider partitionInfoProvider,
			[NotNull] IDeterministicPartitionSelectorHelper deterministicPartitionSelectorHelper,
			DeployedFabricApplication deployedFabricApplication,
			DeployedFabricService deployedFabricService)
		{
			m_proxyProvider = proxyProvider;
			m_partitionInfoProvider = partitionInfoProvider;
			m_deterministicPartitionSelectorHelper = deterministicPartitionSelectorHelper;
			m_random = new Random();

			m_deployedFabricApplication = deployedFabricApplication;
			m_deployedFabricService = deployedFabricService;
		}

		public async Task Invoke(Func<TServiceType, Task> invocationFunc)
		{
			var proxy = await m_proxyProvider.GetServiceProxyAsync<TServiceType>(
				m_deployedFabricService,
				m_deployedFabricApplication,
				servicePartitionKey: await GetRandomPartitionKey());

			await invocationFunc(proxy);
		}

		public async Task<TResult> Invoke<TResult>(Func<TServiceType, Task<TResult>> invocationFunc)
		{
			var proxy = await m_proxyProvider.GetServiceProxyAsync<TServiceType>(
				m_deployedFabricService,
				m_deployedFabricApplication,
				servicePartitionKey: await GetRandomPartitionKey());

			return await invocationFunc(proxy);
		}

		public Task Invoke(
			IPartitionSelector sessionContext,
			Func<TServiceType, Task> invocationFunc)
			=> Invoke(sessionContext.HashableIdentifier, invocationFunc);

		public Task<TResult> Invoke<TResult>(
			IPartitionSelector sessionContext,
			Func<TServiceType, Task<TResult>> invocationFunc)
			=> Invoke(sessionContext.HashableIdentifier, invocationFunc);

		public async Task Invoke(
			int hashableIdentifier,
			[NotNull] Func<TServiceType, Task> invocationFunc)
		{
			var proxy = await m_proxyProvider.GetServiceProxyAsync<TServiceType>(
				m_deployedFabricService,
				m_deployedFabricApplication,
				servicePartitionKey: await ResolveToPartitionKey(hashableIdentifier));

			 await invocationFunc(proxy);
		}

		public async Task<TResult> Invoke<TResult>(
			int hashableIdentifier,
			[NotNull] Func<TServiceType, Task<TResult>> invocationFunc)
		{
			var proxy = await m_proxyProvider.GetServiceProxyAsync<TServiceType>(
				m_deployedFabricService,
				m_deployedFabricApplication,
				servicePartitionKey: await ResolveToPartitionKey(hashableIdentifier));

			return await invocationFunc(proxy);
		}

		[NotNull]
		private Task<long> ResolveToPartitionKey([NotNull] IPartitionSelector sessionContext)
			=> ResolveToPartitionKey(sessionContext.HashableIdentifier);

		private async Task<long> ResolveToPartitionKey(int hashableIdentifier)
		{
			var partitionInformation = await m_partitionInfoProvider.GetPartitionInfoForService(m_deployedFabricService);

			return m_deterministicPartitionSelectorHelper.HashIdentifierToPartitionIntIdentifier(hashableIdentifier, partitionInformation.Range);
		}

		private async Task<long> GetRandomPartitionKey()
		{
			var partitionInformation = await m_partitionInfoProvider.GetPartitionInfoForService(m_deployedFabricService);
			return m_random.Next((int) partitionInformation.Range);
		}
	}
}
