using System.Collections.Generic;
using System.Fabric;
using System.Fabric.Query;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;
using Service = System.Fabric.Query.Service;

namespace MooMed.ServiceRemoting.EndpointResolution
{
    public class ServiceDetail
    {
        public string ServiceName { get; }

        private string ServiceTypeName { get; }

        private ServiceKind ServiceKind { get; }

        private ServicePartitionKind ServicePartitionKind { get; }

        public string ServiceEndpointIp { get; }

        public IEnumerable<ResolvedServiceEndpoint> ServiceEndpoints { get; }

        public ServiceDetail(
            [NotNull] Service service,
            ServicePartitionKind partitionKind, 
            [NotNull] IEnumerable<ResolvedServiceEndpoint> serviceEndpoints)
        {
            ServiceName = service.ServiceName.AbsoluteUri;

            ServiceTypeName = service.ServiceTypeName;
            ServiceKind = service.ServiceKind;
            ServicePartitionKind = partitionKind;

            var resolvedServiceEndpointsList = serviceEndpoints.ToList();
            ServiceEndpoints = resolvedServiceEndpointsList;

            var endpointObjectParsed = JObject.Parse(resolvedServiceEndpointsList.First().Address);
            var endpoint = endpointObjectParsed.First.First.First.First.Value<string>();

            ServiceEndpointIp = endpoint.TrimEnd('/');
        }
    }
}
