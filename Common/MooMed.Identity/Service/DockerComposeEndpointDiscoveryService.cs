using System.Collections.Generic;
using MooMed.Common.Definitions.IPC;
using MooMed.Identity.Service.Interface;

namespace MooMed.Identity.Service
{
	public class DockerComposeEndpointDiscoveryService : IEndpointDiscoveryService
	{
		public Endpoint GetDeploymentEndpoint(DeploymentService deploymentService)
		{
			var dnsName = $"moomed.{deploymentService.ToString().ToLower()}";

			return new()
			{
				DnsName = dnsName,
			};
		}

		public StatefulEndpointCollection GetStatefulEndpoints(StatefulSetService statefulSetService, int totalReplicas = 1)
		{
			var dnsName = $"MooMed.{statefulSetService.ToString().ToLower()}";

			return new StatefulEndpointCollection(new List<StatefulEndpoint>()
			{
				new()
				{
					InstanceNumber = 0,
					DnsName = dnsName,
				}
			});
		}
	}
}