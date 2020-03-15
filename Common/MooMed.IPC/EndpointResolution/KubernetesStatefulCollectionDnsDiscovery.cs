using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Dns.Service.Interface;
using MooMed.IPC.DataType;
using MooMed.IPC.DataType.Kubernetes;
using MooMed.IPC.EndpointResolution.Interface;

namespace MooMed.IPC.EndpointResolution
{
	public class KubernetesStatefulCollectionDnsDiscovery : IStatefulCollectionDiscovery
	{
		[NotNull]
		private readonly IDnsResolutionService m_dnsResolutionService;

		public KubernetesStatefulCollectionDnsDiscovery(
			[NotNull] IDnsResolutionService dnsResolutionService)
		{
			m_dnsResolutionService = dnsResolutionService;
		}

		public async Task<IStatefulCollection> GetStatefulSetInfo(StatefulSet statefulSet, int totalReplicas = 1)
		{
			var endpoints = await Task.WhenAll(
				Enumerable.Range(0, totalReplicas)
				.Select(async replicaNr =>
				{
					var ip = await m_dnsResolutionService.ResolveStatefulSetReplicaToIp(statefulSet, replicaNr);

					return new KubernetesEndpoint()
					{
						InstanceNumber = replicaNr,
						IpAddress = ip.ToString()
					};
				})
			);

			return new KubernetesStatefulSet(endpoints.Where(x => x != null).ToArray());
		}
	}
}
