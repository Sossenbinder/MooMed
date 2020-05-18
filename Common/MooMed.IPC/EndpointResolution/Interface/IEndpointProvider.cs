using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.IPC.DataType;

namespace MooMed.IPC.EndpointResolution.Interface
{
	public interface IEndpointProvider
	{
		[NotNull]
		Endpoint GetDeploymentEndpoint(DeploymentService deploymentService);

		[NotNull]
		StatefulEndpointCollection GetStatefulSetEndpointCollectionInfoForService(StatefulSetService statefulSetService);

		int GetAvailableReplicasForStatefulService(StatefulSetService statefulSetService);
	}
}
