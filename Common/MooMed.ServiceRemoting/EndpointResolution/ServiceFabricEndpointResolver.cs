using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.ServiceFabric.Services.Client;
using MooMed.ServiceFabric.Helper;
using MooMed.ServiceRemoting.DataType;
using MooMed.ServiceRemoting.EndpointResolution.Interface;
using MooMed.ServiceRemoting.Helper;

namespace MooMed.ServiceRemoting.EndpointResolution
{
    public class ServiceFabricEndpointResolver : IServiceEndpointResolver
    {
        [NotNull]
        private readonly FabricClient m_fabricClient;

        [NotNull]
        private readonly Dictionary<DeployedFabricService, ServiceDetail> m_serviceDetailCache;

        public ServiceFabricEndpointResolver()
        {
            m_fabricClient = FabricClientFactory.Create();
			m_serviceDetailCache = new Dictionary<DeployedFabricService, ServiceDetail>();
        }

        public ServiceDetail GetServiceDetail(DeployedFabricService deployedFabricService)
        {
            if (m_serviceDetailCache.GetValueOrDefault(deployedFabricService, null) == null)
            {
                Task.Run(async () => { await RefreshSingleEndpoint(deployedFabricService); }).Wait();
            }

            return m_serviceDetailCache[deployedFabricService];
        }

        public async Task<ServiceDetail> GetServiceDetailAsync(DeployedFabricService deployedFabricService)
        {
            if (m_serviceDetailCache.GetValueOrDefault(deployedFabricService, null) == null)
            {
                await RefreshSingleEndpoint(deployedFabricService);
            }

            return m_serviceDetailCache[deployedFabricService];
        }

        private async Task ResolveEndpoint([NotNull] System.Fabric.Query.Service service)
        {
            foreach (var partition in await m_fabricClient.QueryManager.GetPartitionListAsync(service.ServiceName))
            {
                // Check what kind of service we have - depending on that the resolver figures out the endpoints.
                ServicePartitionKey key;
                switch (partition.PartitionInformation.Kind)
                {
                    case ServicePartitionKind.Singleton:
                        key = ServicePartitionKey.Singleton;
                        break;
                    case ServicePartitionKind.Int64Range:
                        var longKey = (Int64RangePartitionInformation)partition.PartitionInformation;
                        key = new ServicePartitionKey(longKey.LowKey);
                        break;
                    case ServicePartitionKind.Named:
                        var namedKey = (NamedPartitionInformation)partition.PartitionInformation;
                        key = new ServicePartitionKey(namedKey.Name);
                        break;
                    case ServicePartitionKind.Invalid:
                        continue;
                    default:
                        throw new ArgumentOutOfRangeException($"Can't resolve partition kind for partition with id {partition.PartitionInformation.Id}");
                }

                var resolvedServicePartition = await ServicePartitionResolver.GetDefault().ResolveAsync(service.ServiceName, key, CancellationToken.None);

                AddToCache(DeployedFabricServiceHelper.ServiceNameToFabricService(service.ServiceTypeName), new ServiceDetail(service, ServicePartitionKind.Int64Range, resolvedServicePartition.Endpoints));
            }
        }

        /// <summary>
        /// Retrieves endpoints of all running instances in SF. Architecture is like this:
        /// SF -> Application (MooMed.Fabric) -> Service (Deployed services to SF App, e.g. MooMed.Web / MooMed.WorkerMain)
        /// -> Partition (Nodes where a service could be deployed, up to SF to decide) -> Instance (Multiple services could be on one partition)
        /// -> Replica of an instance
        /// 
        /// Stateless services usually don't have partitions but rather multiple instances, as dragging them horizontally makes no sense if no state needs to
        /// be shared, so they have multiple instances instead, each being completely independent.
        ///
        /// Stateful services instead are often partitioned on a key-basis - Each partition would therefore e.g. only service IDs in a certain range. That way, a
        /// stateful service can be multiplied without having conflicts due to unshared state 
        /// </summary>
        public async Task RefreshEndpointList()
        {
            // For an app, we only need the app given here, even if the cluster might possibly have more apps deployed
            var appList = await m_fabricClient.QueryManager.GetApplicationListAsync();
            var app = appList.Single(x => x.ApplicationName.ToString().Contains("MooMed.Fabric"));

            // Go through all running services
            foreach (var service in await m_fabricClient.QueryManager.GetServiceListAsync(app.ApplicationName))
            {
                // Go through all partitions of a service
                await ResolveEndpoint(service);
            }
        }

        private async Task RefreshSingleEndpoint(DeployedFabricService deployedService)
        {
            // For an app, we only need the app given here, even if the cluster might possibly have more apps deployed
            var appList = await m_fabricClient.QueryManager.GetApplicationListAsync();
            var app = appList.Single(x => x.ApplicationName.ToString().Contains("MooMed.Fabric"));

            var endpointAsString = DeployedFabricServiceHelper.ServiceNameToFabricService(deployedService);

            var services = await m_fabricClient.QueryManager.GetServiceListAsync(app.ApplicationName);
            var service = services.SingleOrDefault(x => x.ServiceTypeName.Equals(endpointAsString));
            await ResolveEndpoint(service);
        }

        private void AddToCache(DeployedFabricService key, [NotNull] ServiceDetail serviceDetail)
        {
            m_serviceDetailCache[key] = serviceDetail;
        }
    }
}
