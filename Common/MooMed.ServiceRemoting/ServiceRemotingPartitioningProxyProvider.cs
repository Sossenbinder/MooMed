using System;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;
using MooMed.ServiceRemoting.DataType;
using MooMed.ServiceRemoting.Interface;

namespace MooMed.ServiceRemoting
{
	public class RemotingPartitioningProxyProvider : IRemotingPartitioningProxyProvider
	{
		public TService GetServiceProxy<TService>(DeployedFabricService deployedFabricServiceTarget, DeployedFabricApplication deployedFabricApplication) 
			where TService : IService
		{
			throw new NotImplementedException();
		}

		public Task<TService> GetServiceProxyAsync<TService>(DeployedFabricService deployedFabricServiceTarget, DeployedFabricApplication deployedFabricApplication) 
			where TService : IService
		{
			throw new NotImplementedException();
		}
	}
}
