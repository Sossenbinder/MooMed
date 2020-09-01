using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using k8s;
using Microsoft.Extensions.Hosting;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Logging;
using MooMed.Eventing.Events.MassTransit.Interface;

namespace KubeWatcher
{
	public class Worker : BackgroundService
	{
		[NotNull]
		private readonly IMooMedLogger _logger;

		[NotNull]
		private readonly IMassTransitEventingService _massTransitEventingService;

		[NotNull]
		private readonly Dictionary<StatefulSetService, int> _replicaMap;

		[NotNull]
		private readonly Kubernetes _kubernetesClient;

		public Worker(
			[NotNull] IMooMedLogger logger,
			[NotNull] IMassTransitEventingService massTransitEventingService)
		{
			_logger = logger;
			_massTransitEventingService = massTransitEventingService;
			_replicaMap = new Dictionary<StatefulSetService, int>();

			var config = KubernetesClientConfiguration.InClusterConfig();

			_kubernetesClient = new Kubernetes(config);
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
			{
				await QueryKube(stoppingToken);

				await Task.Delay(5000, stoppingToken);
			}
		}

		private async Task QueryKube(CancellationToken stoppingToken)
		{
			var statefulSets = await _kubernetesClient.ListStatefulSetForAllNamespacesAsync(cancellationToken: stoppingToken);

			foreach (var statefulSet in statefulSets.Items)
			{
				var statefulSetName = statefulSet.Metadata.Name.ToLower().Split('-')[1];

				if (Enum.TryParse(statefulSetName, true, out StatefulSetService deployedService))
				{
					var replicas = statefulSet.Spec.Replicas;

					if (replicas.HasValue)
					{
						await HandleNewStatefulSetInfo(deployedService, replicas.Value);
					}
					else
					{
						_logger.Error($"Statefulset {statefulSet.Metadata.Name} has no replica information attached.");
					}
				}
				else
				{
					_logger.Error($"Failed to resolve {statefulSet.Metadata.Name} to a corresponding StatefulSetService");
				}
			}
		}

		private async Task HandleNewStatefulSetInfo(StatefulSetService statefulSetService, int replicas)
		{
			if (!_replicaMap.ContainsKey(statefulSetService))
			{
				_replicaMap.Add(statefulSetService, replicas);
			}
			else
			{
				var existingReplicasForService = _replicaMap[statefulSetService];

				if (existingReplicasForService != replicas)
				{
					_logger.Error($"Replicas for {statefulSetService.ToString()} changed from {existingReplicasForService} to {replicas}. Emitting event.");

					await _massTransitEventingService.RaiseEvent(new ClusterChangeEvent
					{
						StatefulSetService = statefulSetService,
						NewReplicaAmount = replicas
					});
				}

				_replicaMap[statefulSetService] = replicas;
			}
		}
	}
}