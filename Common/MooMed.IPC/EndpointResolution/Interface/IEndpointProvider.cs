using System.Threading.Tasks;
using MooMed.Common.Definitions.IPC;

namespace MooMed.IPC.EndpointResolution.Interface
{
	public interface IEndpointProvider
	{
		Task<Endpoint> GetDeploymentEndpoint(DeploymentService deploymentService);

		Task<StatefulEndpointCollection> GetStatefulSetEndpointCollectionInfoForService(StatefulSetService statefulSetService);

		Task<int> GetAvailableReplicasForStatefulService(StatefulSetService statefulSetService);
	}
}