using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MooMed.Common.Definitions.IPC;
using MooMed.IPC.DataType;
using MooMed.IPC.DataType.Docker;
using MooMed.IPC.EndpointResolution.Interface;

namespace MooMed.IPC.EndpointResolution
{
	public class DockerComposeStatefulCollectionDiscovery : IStatefulCollectionDiscovery
	{
		public IAsyncEnumerable<(DeployedService, IStatefulCollection)> RefreshForAllStatefulSets()
		{
			throw new NotImplementedException();
		}

		public Task<IStatefulCollection> GetStatefulSetInfo(DeployedService deployedService)
		{
			var ipAddresses = Dns.GetHostEntry($"moomed.stateful.{deployedService.ToString().ToLower()}").AddressList;

			var respectiveContainers = ipAddresses.Select((x, i) => new DockerContainer()
			{
				InstanceNumber = i,
				IpAddress = x.ToString()
			}).ToList();

			return Task.FromResult<IStatefulCollection>(new DockerStatefulSet(respectiveContainers));
		}
	}
}
