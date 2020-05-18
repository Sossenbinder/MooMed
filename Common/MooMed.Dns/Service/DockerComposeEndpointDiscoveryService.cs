﻿using System.Collections.Generic;
using MooMed.Common.Definitions.IPC;
using MooMed.Dns.Service.Interface;
using MooMed.IPC.DataType;

namespace MooMed.Dns.Service
{
	public class DockerComposeEndpointDiscoveryService : IEndpointDiscoveryService
	{
		public Endpoint GetDeploymentEndpoint(DeploymentService deploymentService)
		{
			var ipAddress = $"moomed.deployment.{deploymentService.ToString().ToLower()}";

			return new Endpoint()
			{
				IpAddress = ipAddress,
			};
		}

		public StatefulEndpointCollection GetStatefulEndpoints(StatefulSetService statefulSetService, int totalReplicas = 1)
		{
			var ipAddress = $"moomed.stateful.{statefulSetService.ToString().ToLower()}";

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