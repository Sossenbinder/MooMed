using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Core.Code.Logging.Loggers.Interface;
using MooMed.IPC.DataType;
using MooMed.IPC.DataType.Kubernetes;
using MooMed.IPC.EndpointResolution.Interface;
using MooMed.IPC.Interface;

namespace MooMed.IPC.EndpointResolution
{
	public class KubernetesStatefulCollectionDnsDiscovery : IStatefulCollectionDiscovery
	{
		[NotNull]
		private readonly IDictionary<DeployedService, (int, string)> m_kubernetesStatefulSetNameDict;

		[NotNull]
		private readonly IKubernetesClientFactory m_kubernetesClientFactory;

		[NotNull]
		private readonly IMainLogger m_logger;

		public KubernetesStatefulCollectionDnsDiscovery(
			[NotNull] IKubernetesClientFactory kubernetesClientFactory,
			[NotNull] IMainLogger logger)
		{
			m_kubernetesClientFactory = kubernetesClientFactory;
			m_logger = logger;
			m_kubernetesStatefulSetNameDict = new Dictionary<DeployedService, (int, string)>()
			{
				{ DeployedService.AccountService, (3, "moomed-accountservice")},
				{ DeployedService.ProfilePictureService, (3, "moomed-profilepictureservice")},
				{ DeployedService.SessionService, (3, "moomed-sessionservice")},
			};
		}

		public IAsyncEnumerable<(DeployedService, IStatefulCollection)> RefreshForAllStatefulSets()
		{
			throw new NotImplementedException();
		}

		public async Task<IStatefulCollection> GetStatefulSetInfo(DeployedService deployedService)
		{
			var metaDataForService = m_kubernetesStatefulSetNameDict[deployedService];

			var endpoints = await Task.WhenAll(Enumerable.Range(0, metaDataForService.Item1)
				.Select(x => ResolveIdToEndpoint(x, metaDataForService.Item2)));

			return new KubernetesStatefulSet(endpoints.Where(x => x != null).ToArray());
		}

		private async Task<KubernetesEndpoint> ResolveIdToEndpoint(int id, string serviceName)
		{
			var dnsEntryToQuery = $"{serviceName}-{id}.{serviceName}.default.svc.cluster.local";

			var hostEntry = await Dns.GetHostEntryAsync(dnsEntryToQuery);

			var ip = hostEntry.AddressList.FirstOrDefault();

			if (ip == null)
			{
				m_logger.Fatal($"Failed to resolve IP for dns name {dnsEntryToQuery}", null);

				return null;
			}

			var endpoint = new KubernetesEndpoint()
			{
				InstanceNumber = id,
				IpAddress = ip.ToString()
		};

			return endpoint;
		}
	}
}
