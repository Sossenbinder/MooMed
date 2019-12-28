using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;
using MooMed.ServiceRemoting.DataType;

namespace MooMed.ServiceRemoting.Interface
{
	public interface IRemotingContext
	{
		TService GetProxy<TService>(DeployedFabricService deployedFabricService)
			where TService : IService;

		Task<TService> GetProxyAsync<TService>(DeployedFabricService deployedFabricService)
			where TService : IService;
	}
}
