using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using MooMed.Common.Definitions.IPC;
using MooMed.Core.Code.Logging.Loggers.Interface;

namespace MooMed.Dns.Service
{
	public class KubernetesDnsResolutionService : AbstractDnsResolutionService
	{
		public KubernetesDnsResolutionService([NotNull] IMainLogger logger) 
			: base(logger)
		{
		}

		public override async Task<IPAddress> ResolveDeploymentToIp(Deployment deployment)
		{
			var deploymentNameSanitized = $"moomed-{deployment.ToString().ToLower()}";
			
			var dnsEntryToQuery = $"{deploymentNameSanitized}.default.svc.cluster.local";

			return await ResolveDnsNameToIp(dnsEntryToQuery);
		}

		public override async Task<IPAddress> ResolveStatefulSetReplicaToIp(StatefulSet service, int replica)
		{
			var statefulServiceName = service.ToString().ToLower();

			var dnsEntryToQuery = $"moomed-{statefulServiceName}-{replica}.moomed-{statefulServiceName}.default.svc.cluster.local";

			return await ResolveDnsNameToIp(dnsEntryToQuery);
		}
	}
}
