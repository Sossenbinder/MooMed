using System;
using System.Threading.Tasks;
using MooMed.ServiceRemoting.DataType;

namespace MooMed.ServiceRemoting.EndpointResolution.Interface
{
	public interface IServiceFabricServiceResolver
	{
		Task<Uri> ResolveServiceToUri(DeployedFabricService deployedFabricService, DeployedFabricApplication deployedFabricApplication);
	}
}
