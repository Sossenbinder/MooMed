using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Core.Code.Logging.Loggers.Interface;
using MooMed.Dns.Service.Interface;

namespace MooMed.Dns.Service
{
	public class DnsResolutionService : IDnsResolutionService
	{
		[NotNull]
		private readonly IMainLogger _logger;

		public DnsResolutionService([NotNull] IMainLogger logger)
		{
			_logger = logger;
		}

		public async Task<IPAddress> ResolveDnsNameToIp(string name)
		{
			try
			{
				var hostEntry = await System.Net.Dns.GetHostEntryAsync(name);

				var ip = hostEntry.AddressList.FirstOrDefault();

				if (ip == null)
				{
					_logger.Fatal($"Failed to resolve IP for dns name {name}");

					return null;
				}

				_logger.Info($"Successfully queried {name} to IP {ip}");

				return ip;
			}
			catch (SocketException)
			{
				_logger.Fatal($"Failed to resolve IP for dns name {name}");

				return null;
			}
		}

		public string GetOwnHostName()
		{
			return System.Net.Dns.GetHostName();
		}
	}
}
