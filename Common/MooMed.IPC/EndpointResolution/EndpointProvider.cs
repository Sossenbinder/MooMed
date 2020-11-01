using System.Reflection;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Caching.Cache.CacheImplementations.Interface;
using MooMed.Caching.Cache.Factory;
using MooMed.Caching.Cache.Factory.Interface;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Logging;
using MooMed.Identity.Service.Interface;
using MooMed.Eventing.Events.MassTransit.Interface;
using MooMed.IPC.EndpointResolution.Interface;

namespace MooMed.IPC.EndpointResolution
{
	/// <summary>
	/// Gets and provides info about a given stateful set with functionality to refresh it
	/// </summary>
	public class EndpointProvider : IEndpointProvider
	{
		[NotNull]
		private readonly IEndpointDiscoveryService _endpointDiscoveryService;

		[NotNull]
		private readonly IMooMedLogger _logger;

		[NotNull]
		private readonly ICache<DeploymentService, Endpoint> _deploymentEndpointCache;

		[NotNull]
		private readonly ICache<StatefulSetService, StatefulEndpointCollection> _statefulServiceReplicaCache;

		public EndpointProvider(
			[NotNull] IEndpointDiscoveryService endpointDiscoveryService,
			[NotNull] ILocalCacheFactory cacheFactory,
			[NotNull] IMassTransitEventingService massTransitEventingService,
			[NotNull] IMooMedLogger logger)
		{
			_endpointDiscoveryService = endpointDiscoveryService;
			_logger = logger;

			_deploymentEndpointCache = cacheFactory.CreateCache<DeploymentService, Endpoint>();
			_statefulServiceReplicaCache = cacheFactory.CreateCache<StatefulSetService, StatefulEndpointCollection>();

			massTransitEventingService.RegisterForEvent<ClusterChangeEvent>("ClusterChanges_Queue", OnClusterChange);
		}

		private async Task OnClusterChange([NotNull] ClusterChangeEvent changeEvent)
		{
			_logger.Info("Received cluster change event");
			var statefulSet = changeEvent.StatefulSetService;

			var statefulSetInfo = _endpointDiscoveryService.GetStatefulEndpoints(statefulSet, changeEvent.NewReplicaAmount);

			await _statefulServiceReplicaCache.PutItem(statefulSet, statefulSetInfo);

			_logger.Info($"Updated internal stateful info provider to {changeEvent.NewReplicaAmount} instances of {statefulSet} on setService {Assembly.GetExecutingAssembly()}");
		}

		public async Task<Endpoint> GetDeploymentEndpoint(DeploymentService deploymentService)
		{
			var existingDeploymentEndpoint = await _deploymentEndpointCache.GetItem(deploymentService);

			if (existingDeploymentEndpoint != null)
			{
				return existingDeploymentEndpoint;
			}

			var newDeploymentEndpoint = _endpointDiscoveryService.GetDeploymentEndpoint(deploymentService);

			await _deploymentEndpointCache.PutItem(deploymentService, newDeploymentEndpoint);

			return newDeploymentEndpoint;
		}

		public async Task<StatefulEndpointCollection> GetStatefulSetEndpointCollectionInfoForService(StatefulSetService statefulSetService)
		{
			var existingStatefulSetInfo = await _statefulServiceReplicaCache.GetItem(statefulSetService);

			if (existingStatefulSetInfo != null)
			{
				return existingStatefulSetInfo;
			}

			var statefulSetInfo = _endpointDiscoveryService.GetStatefulEndpoints(statefulSetService);

			await _statefulServiceReplicaCache.PutItem(statefulSetService, statefulSetInfo);

			return statefulSetInfo;
		}

		public async Task<int> GetAvailableReplicasForStatefulService(StatefulSetService statefulSetService)
		{
			var statefulEndpointCollection = await GetStatefulSetEndpointCollectionInfoForService(statefulSetService);

			return statefulEndpointCollection.ReplicaCount;
		}
	}
}