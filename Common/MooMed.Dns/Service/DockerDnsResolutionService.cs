using System.Net;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Core.Code.Logging.Loggers.Interface;

namespace MooMed.Dns.Service
{
	public class DockerDnsResolutionService : AbstractDnsResolutionService
	{
		public DockerDnsResolutionService([NotNull] IMainLogger logger) 
			: base(logger)
		{
		}

		public override Task<IPAddress> ResolveDeploymentToIp(Deployment deployment)
		{
			return ResolveDnsNameToIp($"moomed-{deployment.ToString().ToLower()}");
		}

		public override Task<IPAddress> ResolveStatefulSetReplicaToIp(StatefulSet service, int replica)
		{
			return ResolveDnsNameToIp($"moomed.stateful.{service.ToString().ToLower()}");
		}
	}
}
