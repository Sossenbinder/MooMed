using System.Reflection;
using JetBrains.Annotations;
using MooMed.Caching.Cache.CacheImplementations.Interface;
using MooMed.Caching.Cache.Factory;
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
			[NotNull] IDefaultCacheFactory cacheFactory,
			[NotNull] IMassTransitEventingService massTransitEventingService,
			[NotNull] IMooMedLogger logger)
		{
			_endpointDiscoveryService = endpointDiscoveryService;
			_logger = logger;

			_deploymentEndpointCache = cacheFactory.CreateCache<DeploymentService, Endpoint>();
			_statefulServiceReplicaCache = cacheFactory.CreateCache<StatefulSetService, StatefulEndpointCollection>();

			massTransitEventingService.RegisterForEvent<ClusterChangeEvent>("ClusterChanges_Queue", OnClusterChange);
		}

		private void OnClusterChange([NotNull] ClusterChangeEvent changeEvent)
		{
			_logger.Info("Received cluster change event");
			var statefulSet = changeEvent.StatefulSetService;

			var statefulSetInfo = _endpointDiscoveryService.GetStatefulEndpoints(statefulSet, changeEvent.NewReplicaAmount);

			_statefulServiceReplicaCache.PutItem(statefulSet, statefulSetInfo);

			_logger.Info($"Updated internal stateful info provider to {changeEvent.NewReplicaAmount} instances of {statefulSet} on setService {Assembly.GetExecutingAssembly()}");
		}

		public Endpoint GetDeploymentEndpoint(DeploymentService deploymentService)
		{
			var existingDeploymentEndpoint = _deploymentEndpointCache.GetItem(deploymentService);

			if (existingDeploymentEndpoint != null)
			{
				return existingDeploymentEndpoint;
			}

			var newDeploymentEndpoint = _endpointDiscoveryService.GetDeploymentEndpoint(deploymentService);

			_deploymentEndpointCache.PutItem(deploymentService, newDeploymentEndpoint);

			return newDeploymentEndpoint;
		}

		public StatefulEndpointCollection GetStatefulSetEndpointCollectionInfoForService(StatefulSetService statefulSetService)
		{
			var existingStatefulSetInfo = _statefulServiceReplicaCache.GetItem(statefulSetService);

			if (existingStatefulSetInfo != null)
			{
				return existingStatefulSetInfo;
			}

			var statefulSetInfo = _endpointDiscoveryService.GetStatefulEndpoints(statefulSetService);

			_statefulServiceReplicaCache.PutItem(statefulSetService, statefulSetInfo);

			return statefulSetInfo;
		}

		public int GetAvailableReplicasForStatefulService(StatefulSetService statefulSetService)
		{
			var statefulEndpointCollection = GetStatefulSetEndpointCollectionInfoForService(statefulSetService);

			return statefulEndpointCollection.ReplicaCount;
		}
	}
}