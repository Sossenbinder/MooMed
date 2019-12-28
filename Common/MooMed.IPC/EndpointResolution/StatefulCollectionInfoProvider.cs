using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Caching.Cache.CacheImplementations.Interface;
using MooMed.Caching.Cache.Factory;
using MooMed.Common.Definitions.IPC;
using MooMed.IPC.DataType;
using MooMed.IPC.DataType.Kubernetes;
using MooMed.IPC.EndpointResolution.Interface;

namespace MooMed.IPC.EndpointResolution
{
	/// <summary>
	/// Gets Provides info about a given kubernetes stateful set with functionality to refresh it
	/// </summary>
	public class StatefulCollectionInfoProvider : IStatefulCollectionInfoProvider
	{
		[NotNull]
		private readonly IKubernetesDiscovery m_kubernetesDiscovery;

		[NotNull]
		private readonly ICache<DeployedService, IStatefulCollection> m_statefulCollectionCache;

		public StatefulCollectionInfoProvider(
			[NotNull] IKubernetesDiscovery kubernetesDiscovery,
			[NotNull] IDefaultCacheFactory cacheFactory)
		{
			m_kubernetesDiscovery = kubernetesDiscovery;

			m_statefulCollectionCache = cacheFactory.CreateCache<DeployedService, IStatefulCollection>();
		}

		public async Task<IStatefulCollection> GetStatefulCollectionInfoForService(DeployedService deployedService)
		{
			var existingStatefulSetInfo = m_statefulCollectionCache.GetItem(deployedService);

			if (existingStatefulSetInfo != null)
			{
				return existingStatefulSetInfo;
			}

			var statefulSetInfo = await m_kubernetesDiscovery.GetStatefulSetInfo(deployedService);
			
			m_statefulCollectionCache.PutItem(deployedService, statefulSetInfo);

			return statefulSetInfo;
		}

		public async Task<int> GetAvailableReplicasForService(DeployedService deployedService)
		{
			var statefulSet = await GetStatefulCollectionInfoForService(deployedService);

			return statefulSet.ReplicaCount;
		}
	}
}
