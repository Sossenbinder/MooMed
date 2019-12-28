using System.Fabric.Query;
using System.Threading.Tasks;
using MooMed.ServiceRemoting.DataType;

namespace MooMed.ServiceRemoting.EndpointResolution.Interface
{
	public interface IServiceFabricEndpointManager
	{
		Task<Service> GetServiceOnApp(DeployedFabricService deployedFabricService, DeployedFabricApplication deployedFabricApplication = DeployedFabricApplication.MooMed);
	}
}
