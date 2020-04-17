using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Dns.Service.Interface;
using MooMed.IPC.DataType;
using MooMed.IPC.DataType.Docker;
using MooMed.IPC.EndpointResolution.Interface;

namespace MooMed.IPC.EndpointResolution
{
	public class DockerComposeStatefulCollectionDiscovery : IStatefulCollectionDiscovery
	{
		[NotNull]
		private readonly IDnsResolutionService _dnsResolutionService;

		public DockerComposeStatefulCollectionDiscovery([NotNull] IDnsResolutionService dnsResolutionService)
		{
			_dnsResolutionService = dnsResolutionService;
		}

		public async Task<IStatefulCollection> GetStatefulSetInfo(StatefulSet statefulSet, int totalReplicas = 1)
		{
			var ipAddress = await _dnsResolutionService.ResolveStatefulSetReplicaToIp(statefulSet, totalReplicas);
			
			return new DockerStatefulSet(new List<DockerContainer>(){ new DockerContainer()
			{
				InstanceNumber = 0,
				IpAddress = ipAddress.ToString()
			}});
		}
	}
}
