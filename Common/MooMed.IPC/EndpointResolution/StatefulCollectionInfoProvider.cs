using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Caching.Cache.CacheImplementations.Interface;
using MooMed.Caching.Cache.Factory;
using MooMed.Common.Definitions.IPC;
using MooMed.Core.Code.Logging.Loggers.Interface;
using MooMed.Eventing.Events.MassTransit.Interface;
using MooMed.IPC.DataType;
using MooMed.IPC.EndpointResolution.Interface;

namespace MooMed.IPC.EndpointResolution
{
	/// <summary>
	/// Gets and provides info about a given stateful set with functionality to refresh it
	/// </summary>
	public class StatefulCollectionInfoProvider : IStatefulCollectionInfoProvider
	{
		[NotNull]
		private readonly IStatefulCollectionDiscovery m_statefulCollectionDiscovery;

		[NotNull]
		private readonly IMainLogger m_mainLogger;

		[NotNull]
		private readonly ICache<StatefulSet, IStatefulCollection> m_statefulCollectionCache;

		public StatefulCollectionInfoProvider(
			[NotNull] IStatefulCollectionDiscovery statefulCollectionDiscovery,
			[NotNull] IDefaultCacheFactory cacheFactory,
			[NotNull] IMassTransitEventingService massTransitEventingService,
			[NotNull] IMainLogger mainLogger)
		{
			m_statefulCollectionDiscovery = statefulCollectionDiscovery;
			m_mainLogger = mainLogger;

			m_statefulCollectionCache = cacheFactory.CreateCache<StatefulSet, IStatefulCollection>();

			massTransitEventingService.RegisterForEvent<ClusterChangeEvent>("ClusterChanges_Queue", OnClusterChange);
		}

		private async Task OnClusterChange([NotNull] ClusterChangeEvent changeEvent)
		{
			m_mainLogger.Info("Received cluster change event");
			var statefulSet = changeEvent.StatefulSet;

			var statefulSetInfo = await m_statefulCollectionDiscovery.GetStatefulSetInfo(statefulSet, changeEvent.NewReplicaAmount);

			m_statefulCollectionCache.PutItem(statefulSet, statefulSetInfo);

			m_mainLogger.Info($"Updated internal stateful info provider to {changeEvent.NewReplicaAmount} instances of {statefulSet} on service {Assembly.GetExecutingAssembly()}");
		}

		public async Task<IStatefulCollection> GetStatefulCollectionInfoForService(StatefulSet statefulSet)
		{
			var existingStatefulSetInfo = m_statefulCollectionCache.GetItem(statefulSet);

			if (existingStatefulSetInfo != null)
			{
				return existingStatefulSetInfo;
			}

			var statefulSetInfo = await m_statefulCollectionDiscovery.GetStatefulSetInfo(statefulSet);
			
			m_statefulCollectionCache.PutItem(statefulSet, statefulSetInfo);

			return statefulSetInfo;
		}

		public async Task<int> GetAvailableReplicasForService(StatefulSet statefulSet)
		{
			var statefulSetCollection = await GetStatefulCollectionInfoForService(statefulSet);

			return statefulSetCollection.ReplicaCount;
		}
	}
}
