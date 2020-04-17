using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using k8s;
using Microsoft.Extensions.Hosting;
using MooMed.Common.Definitions.IPC;
using MooMed.Core.Code.Logging.Loggers.Interface;
using MooMed.Eventing.Events.MassTransit.Interface;

namespace KubeWatcher
{
	public class Worker : BackgroundService
	{
		[NotNull]
		private readonly IMainLogger _mainLogger;

		[NotNull]
		private readonly IMassTransitEventingService _massTransitEventingService;

		[NotNull]
		private readonly Dictionary<StatefulSet, int> _replicaMap;

		[NotNull]
		private readonly Kubernetes _kubernetesClient;
		
		public Worker(
			[NotNull] IMainLogger mainLogger,
			[NotNull] IMassTransitEventingService massTransitEventingService)
		{
			_mainLogger = mainLogger;
			_massTransitEventingService = massTransitEventingService;
			_replicaMap = new Dictionary<StatefulSet, int>();

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

				if (Enum.TryParse(statefulSetName, true, out StatefulSet deployedService))
				{
					var replicas = statefulSet.Spec.Replicas;

					if (replicas.HasValue)
					{
						await HandleNewStatefulSetInfo(deployedService, replicas.Value);
					}
					else
					{
						_mainLogger.Error($"Statefulset {statefulSet.Metadata.Name} has no replica information attached.");
					}
				}
				else
				{
					_mainLogger.Error($"Failed to resolve {statefulSet.Metadata.Name} to a corresponding StatefulSet");
				}
			}
		}

		private async Task HandleNewStatefulSetInfo(StatefulSet statefulSet, int replicas)
		{
			if (!_replicaMap.ContainsKey(statefulSet))
			{
				_replicaMap.Add(statefulSet, replicas);
			}
			else
			{
				var existingReplicasForService = _replicaMap[statefulSet];

				if (existingReplicasForService != replicas)
				{
					_mainLogger.Error($"Replicas for {statefulSet.ToString()} changed from {existingReplicasForService} to {replicas}. Emitting event.");

					await _massTransitEventingService.RaiseEvent(new ClusterChangeEvent
					{
						StatefulSet = statefulSet,
						NewReplicaAmount = replicas
					});

				}

				_replicaMap[statefulSet] = replicas;
			}
		}
	}
}
