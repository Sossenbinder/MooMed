using System.Net;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;

namespace MooMed.Dns.Service.Interface
{
	public interface IDnsResolutionService
	{
		Task<IPAddress> ResolveDnsNameToIp([NotNull] string name);

		Task<IPAddress> ResolveDeploymentToIp(Deployment deployment);

		Task<IPAddress> ResolveStatefulSetReplicaToIp(StatefulSet service, int replica);
	}
}
