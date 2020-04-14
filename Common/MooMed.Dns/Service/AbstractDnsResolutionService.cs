using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Core.Code.Logging.Loggers.Interface;
using MooMed.Dns.Service.Interface;

namespace MooMed.Dns.Service
{
	public abstract class AbstractDnsResolutionService : IDnsResolutionService
	{
		[NotNull]
		private readonly IMainLogger m_logger;

		protected AbstractDnsResolutionService([NotNull] IMainLogger logger)
		{
			m_logger = logger;
		}

		public async Task<IPAddress> ResolveDnsNameToIp(string name)
		{
			try
			{
				var hostEntry = await System.Net.Dns.GetHostEntryAsync(name);

				var ip = hostEntry.AddressList.FirstOrDefault();

				if (ip == null)
				{
					m_logger.Fatal($"Failed to resolve IP for dns name {name}");

					return null;
				}

				m_logger.Info($"Successfully queried {name} to IP {ip}");

				return ip;
			}
			catch (SocketException)
			{
				m_logger.Fatal($"Failed to resolve IP for dns name {name}");

				return null;
			}
		}

		public abstract Task<IPAddress> ResolveDeploymentToIp(Deployment deployment);

		public abstract Task<IPAddress> ResolveStatefulSetReplicaToIp(StatefulSet service, int replica);

		public string GetOwnHostName()
		{
			return System.Net.Dns.GetHostName();
		}
	}
}
