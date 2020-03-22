﻿using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Dns.Service.Interface;
using MooMed.IPC.DataType;
using MooMed.IPC.DataType.Docker;
using MooMed.IPC.EndpointResolution.Interface;

namespace MooMed.IPC.EndpointResolution
{
	public class AbstractStatefulCollectionDiscovery : IStatefulCollectionDiscovery
	{
		[NotNull]
		private readonly IDnsResolutionService m_dnsResolutionService;

		public AbstractStatefulCollectionDiscovery([NotNull] IDnsResolutionService dnsResolutionService)
		{
			m_dnsResolutionService = dnsResolutionService;
		}

		public async Task<IStatefulCollection> GetStatefulSetInfo(StatefulSet statefulSet, int totalReplicas = 1)
		{
			var ipAddress = await m_dnsResolutionService.ResolveStatefulSetReplicaToIp(statefulSet, 0);

			return new DockerStatefulSet(new List<DockerContainer>(){ new DockerContainer()
			{
				InstanceNumber = 0,
				IpAddress = ipAddress.ToString()
			}});
		}
	}
}