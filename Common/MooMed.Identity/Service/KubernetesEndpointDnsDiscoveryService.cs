using System.Linq;
using MooMed.Common.Definitions.IPC;
using MooMed.Identity.Service.Interface;

namespace MooMed.Identity.Service
{
	public class KubernetesEndpointDnsDiscoveryService : IEndpointDiscoveryService
	{
		public Endpoint GetDeploymentEndpoint(DeploymentService deploymentService)
		{
			var deploymentNameSanitized = $"moomed-{deploymentService.ToString().ToLower()}";

			var dnsEntry = $"{deploymentNameSanitized}.default.svc.cluster.local";

			return new Endpoint
			{
				IpAddress = dnsEntry,
			};
		}
		
		public StatefulEndpointCollection GetStatefulEndpoints(StatefulSetService statefulSetService, int totalReplicas = 1)
		{
			var statefulServiceName = statefulSetService.ToString().ToLower();

			var endpoints = Enumerable.Range(0, totalReplicas)
				.Select(replicaNr =>
				{
					var dns = $"moomed-{statefulServiceName}-{replicaNr}.moomed-{statefulServiceName}.default.svc.cluster.local";

					return new StatefulEndpoint
					{
						InstanceNumber = replicaNr,
						IpAddress = dns,
					};
				});
			

			return new StatefulEndpointCollection(endpoints.ToList());
		}
	}
}
