using System.Collections.Generic;
using MooMed.Common.Definitions.IPC;
using MooMed.Identity.Service.Interface;

namespace MooMed.Identity.Service
{
	public class DockerComposeEndpointDiscoveryService : IEndpointDiscoveryService
	{
		public Endpoint GetDeploymentEndpoint(DeploymentService deploymentService)
		{
			var ipAddress = $"moomed.{deploymentService.ToString().ToLower()}";

			return new Endpoint()
			{
				IpAddress = ipAddress,
			};
		}

		public StatefulEndpointCollection GetStatefulEndpoints(StatefulSetService statefulSetService, int totalReplicas = 1)
		{
			var ipAddress = $"MooMed.{statefulSetService.ToString().ToLower()}";

			return new StatefulEndpointCollection(new List<StatefulEndpoint>()
			{
				new StatefulEndpoint()
				{
					InstanceNumber = 0,
					IpAddress = ipAddress.ToString(),
				}
			});
		}
	}
}
