using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using MooMed.ServiceRemoting.DataType;
using MooMed.ServiceRemoting.EndpointResolution.Interface;
using MooMed.ServiceRemoting.Interface;

namespace MooMed.ServiceRemoting
{
    public class RemotingProxyProvider : IRemotingProxyProvider
    {
        private IServiceFabricServiceResolver m_serviceFabricServiceResolver { get; }

        public RemotingProxyProvider([NotNull] IServiceFabricServiceResolver serviceFabricServiceResolver)
        {
	        m_serviceFabricServiceResolver = serviceFabricServiceResolver;
        }

        public TService GetServiceProxy<TService>(DeployedFabricService deployedFabricServiceTarget, DeployedFabricApplication deployedFabricApplication,
	        Uri serviceUri = null, long servicePartitionKey = 1)
            where TService : IService
        {
            if (serviceUri == null)
            {
                serviceUri = m_serviceFabricServiceResolver.ResolveServiceToUri(deployedFabricServiceTarget, deployedFabricApplication).GetAwaiter().GetResult();
            }

            return ServiceProxy.Create<TService>(serviceUri, new ServicePartitionKey(servicePartitionKey));
        }

        [ItemCanBeNull]
        public async Task<TService> GetServiceProxyAsync<TService>(DeployedFabricService deployedFabricServiceTarget, DeployedFabricApplication deployedFabricApplication,
	        Uri serviceUri = null, long servicePartitionKey = 1)
            where TService : IService
        {
            if (serviceUri == null)
            {
                serviceUri = await m_serviceFabricServiceResolver.ResolveServiceToUri(deployedFabricServiceTarget, deployedFabricApplication);
            }

            return ServiceProxy.Create<TService>(serviceUri, new ServicePartitionKey(servicePartitionKey));
        }
    }
}
