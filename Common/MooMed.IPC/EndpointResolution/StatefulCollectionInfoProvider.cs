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
		private readonly IStatefulCollectionDiscovery _statefulCollectionDiscovery;

		[NotNull]
		private readonly IMainLogger _mainLogger;

		[NotNull]
		private readonly ICache<StatefulSet, IStatefulCollection> _statefulCollectionCache;

		public StatefulCollectionInfoProvider(
			[NotNull] IStatefulCollectionDiscovery statefulCollectionDiscovery,
			[NotNull] IDefaultCacheFactory cacheFactory,
			[NotNull] IMassTransitEventingService massTransitEventingService,
			[NotNull] IMainLogger mainLogger)
		{
			_statefulCollectionDiscovery = statefulCollectionDiscovery;
			_mainLogger = mainLogger;

			_statefulCollectionCache = cacheFactory.CreateCache<StatefulSet, IStatefulCollection>();

			massTransitEventingService.RegisterForEvent<ClusterChangeEvent>("ClusterChanges_Queue", OnClusterChange);
		}

		private async Task OnClusterChange([NotNull] ClusterChangeEvent changeEvent)
		{
			_mainLogger.Info("Received cluster change event");
			var statefulSet = changeEvent.StatefulSet;

			var statefulSetInfo = await _statefulCollectionDiscovery.GetStatefulSetInfo(statefulSet, changeEvent.NewReplicaAmount);

			_statefulCollectionCache.PutItem(statefulSet, statefulSetInfo);

			_mainLogger.Info($"Updated internal stateful info provider to {changeEvent.NewReplicaAmount} instances of {statefulSet} on service {Assembly.GetExecutingAssembly()}");
		}

		public async Task<IStatefulCollection> GetStatefulCollectionInfoForService(StatefulSet statefulSet)
		{
			var existingStatefulSetInfo = _statefulCollectionCache.GetItem(statefulSet);

			if (existingStatefulSetInfo != null)
			{
				return existingStatefulSetInfo;
			}

			var statefulSetInfo = await _statefulCollectionDiscovery.GetStatefulSetInfo(statefulSet);
			
			_statefulCollectionCache.PutItem(statefulSet, statefulSetInfo);

			return statefulSetInfo;
		}

		public async Task<int> GetAvailableReplicasForService(StatefulSet statefulSet)
		{
			var statefulSetCollection = await GetStatefulCollectionInfoForService(statefulSet);

			return statefulSetCollection.ReplicaCount;
		}
	}
}
