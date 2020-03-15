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
		private readonly IMainLogger m_mainLogger;

		[NotNull]
		private readonly IMassTransitEventingService m_massTransitEventingService;

		[NotNull]
		private readonly Dictionary<StatefulSet, int> m_replicaMap;

		[NotNull]
		private readonly Kubernetes m_kubernetesClient;
		
		public Worker(
			[NotNull] IMainLogger mainLogger,
			[NotNull] IMassTransitEventingService massTransitEventingService)
		{
			m_mainLogger = mainLogger;
			m_massTransitEventingService = massTransitEventingService;
			m_replicaMap = new Dictionary<StatefulSet, int>();

			var config = KubernetesClientConfiguration.InClusterConfig();

			m_kubernetesClient = new Kubernetes(config);
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
			var statefulSets = await m_kubernetesClient.ListStatefulSetForAllNamespacesAsync(cancellationToken: stoppingToken);

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
						m_mainLogger.Error($"Statefulset {statefulSet.Metadata.Name} has no replica information attached.");
					}
				}
				else
				{
					m_mainLogger.Error($"Failed to resolve {statefulSet.Metadata.Name} to a corresponding StatefulSet");
				}
			}
		}

		private async Task HandleNewStatefulSetInfo(StatefulSet statefulSet, int replicas)
		{
			if (!m_replicaMap.ContainsKey(statefulSet))
			{
				m_replicaMap.Add(statefulSet, replicas);
			}
			else
			{
				var existingReplicasForService = m_replicaMap[statefulSet];

				if (existingReplicasForService != replicas)
				{
					m_mainLogger.Error($"Replicas for {statefulSet.ToString()} changed from {existingReplicasForService} to {replicas}. Emitting event.");

					await m_massTransitEventingService.RaiseEvent(new ClusterChangeEvent
					{
						StatefulSet = statefulSet,
						NewReplicaAmount = replicas
					});

				}

				m_replicaMap[statefulSet] = replicas;
			}
		}
	}
}
