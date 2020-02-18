using System;
using k8s;
using MooMed.IPC.Interface;

namespace MooMed.IPC
{
	public class KubernetesClientFactory : IKubernetesClientFactory
	{
		private readonly Lazy<Kubernetes> m_kubernetesClient;

		public KubernetesClientFactory()
		{
			m_kubernetesClient = new Lazy<Kubernetes>(() =>
			{
				// Load from in-cluster configuration:
				var config = KubernetesClientConfiguration.InClusterConfig();

				// Use the config object to create a client.
				return new Kubernetes(config);
			});
		}

		public Kubernetes GetClient() => m_kubernetesClient.Value;
	}
}
