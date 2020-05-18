using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.IPC.DataType;

namespace MooMed.Dns.Service.Interface
{
	public interface IEndpointDiscoveryService
	{
		[NotNull]
		Endpoint GetDeploymentEndpoint(DeploymentService deploymentService);

		[NotNull]
		StatefulEndpointCollection GetStatefulEndpoints(StatefulSetService statefulSetService, int totalReplicas = 1);
	}
}
