using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.ServiceFabric.Services.Remoting;
using MooMed.Caching.Cache.CacheImplementations;
using MooMed.Caching.Cache.CacheImplementations.Interface;
using MooMed.Caching.Cache.CacheInformation;
using MooMed.ServiceRemoting.DataType;
using MooMed.ServiceRemoting.Interface;

namespace MooMed.ServiceRemoting
{
	public abstract class RemotingContextBase : IRemotingContext
	{
		private readonly DeployedFabricApplication m_deployedFabricApplication;

		[NotNull]
		private readonly IRemotingProxyProvider m_remotingProxyProvider;

		[NotNull] 
		private readonly ICache<DeployedFabricService, IService> m_proxyCache;

		protected RemotingContextBase(
			[NotNull] IRemotingProxyProvider remotingProxyProvider,
			DeployedFabricApplication applicationType)
		{
			m_remotingProxyProvider = remotingProxyProvider;
			m_deployedFabricApplication = applicationType;

			m_proxyCache = new ObjectCache<DeployedFabricService, IService>(CacheSettingsProvider.ProxyCacheSettings);
		}

		public TService GetProxy<TService>(DeployedFabricService deployedFabricService)
			where TService : IService
		{
			return GetProxyAsync<TService>(deployedFabricService).Result;
		}

		public async Task<TService> GetProxyAsync<TService>(DeployedFabricService deployedFabricService)
			where TService : IService
		{
			if (m_proxyCache.HasValue(deployedFabricService))
			{
				return (TService)m_proxyCache.GetItem(deployedFabricService);
			}

			var proxy = await m_remotingProxyProvider.GetServiceProxyAsync<TService>(deployedFabricService, m_deployedFabricApplication);

			m_proxyCache[deployedFabricService] = proxy;

			return proxy;
		}
	}
}
