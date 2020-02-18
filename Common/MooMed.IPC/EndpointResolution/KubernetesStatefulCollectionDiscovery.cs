using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using k8s;
using MooMed.Common.Definitions.IPC;
using MooMed.IPC.DataType;
using MooMed.IPC.DataType.Kubernetes;
using MooMed.IPC.EndpointResolution.Interface;
using MooMed.IPC.Exception;
using MooMed.IPC.Interface;

namespace MooMed.IPC.EndpointResolution
{
	/// <summary>
	/// Takes care of retrieving partitioning info from kubernetes
	/// </summary>
	public class KubernetesStatefulCollectionDiscovery : IStatefulCollectionDiscovery
	{
		[NotNull]
		private readonly Kubernetes m_kubernetesClient;
		
		public KubernetesStatefulCollectionDiscovery([NotNull] IKubernetesClientFactory kubernetesClientFactory)
		{
			m_kubernetesClient = kubernetesClientFactory.GetClient();
		}

		public async IAsyncEnumerable<(DeployedService, IStatefulCollection)> RefreshForAllStatefulSets()
		{
			foreach (var service in (DeployedService[])Enum.GetValues(typeof(DeployedService)))
			{
				yield return (service, await GetStatefulSetInfo(service));
			}
		}

		public async Task<IStatefulCollection> GetStatefulSetInfo(DeployedService deployedService)
		{
			// Get all available stateful sets
			var statefulSetResult = await m_kubernetesClient.ListStatefulSetForAllNamespaces1Async();

			// Find the stateful set we are actually interested in
			var respectiveStatefulSet = statefulSetResult.Items.SingleOrDefault(x => x.Metadata.Name.Equals(deployedService.ToString()));

			if (respectiveStatefulSet != null)
			{
				// Now, find all the pods this StatefulSet is currently handling - Labels must match
				var statefulSetTemplateLabels = respectiveStatefulSet.Spec.Template.Metadata.Labels;
				var podList = await m_kubernetesClient.ListPodForAllNamespacesAsync();

				var affectedPods = podList.Items.Select(x => x).Where(x => x.Metadata.Labels.SequenceEqual(statefulSetTemplateLabels));

				var pods = new List<KubernetesStatefulEndpoint>();
				foreach (var affectedPod in affectedPods)
				{
					pods.Add(new KubernetesStatefulEndpoint()
					{
						IpAddress = affectedPod.Status.PodIP,
						StatefulSetPodInstance = 1
					});
				}

				return new KubernetesStatefulSet(pods);
			}

			throw new KubernetesComponentNotFoundException($"Couldn't find service {deployedService.ToString()}");
		}
	}
}
