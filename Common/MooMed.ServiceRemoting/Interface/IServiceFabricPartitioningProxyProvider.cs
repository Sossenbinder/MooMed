
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;
using MooMed.ServiceRemoting.DataType;

namespace MooMed.ServiceRemoting.Interface
{
	public interface IRemotingPartitioningProxyProvider
	{
		TService GetServiceProxy<TService>(DeployedFabricService deployedFabricServiceTarget, DeployedFabricApplication deployedFabricApplication) where TService : IService;

		Task<TService> GetServiceProxyAsync<TService>(DeployedFabricService deployedFabricServiceTarget, DeployedFabricApplication deployedFabricApplication) where TService : IService;
	}
}
