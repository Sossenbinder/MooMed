using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.ServiceFabric.Services.Remoting;
using MooMed.ServiceRemoting.DataType;

namespace MooMed.ServiceRemoting.Interface
{
    public interface IRemotingProxyProvider
    {
        TService GetServiceProxy<TService>(DeployedFabricService deployedFabricServiceTarget, DeployedFabricApplication deployedFabricApplication,
	        [CanBeNull] Uri serviceUri = null, long servicePartitionKey = 1) where TService : IService;

        Task<TService> GetServiceProxyAsync<TService>(DeployedFabricService deployedFabricServiceTarget, DeployedFabricApplication deployedFabricApplication, 
	        [CanBeNull] Uri serviceUri = null, long servicePartitionKey = 1) where TService : IService;
    }
}
