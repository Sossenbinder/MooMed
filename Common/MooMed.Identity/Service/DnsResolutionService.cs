using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Logging;
using MooMed.Identity.Service.Interface;

namespace MooMed.Identity.Service
{
	public class DnsResolutionService : IDnsResolutionService
	{
		[NotNull]
		private readonly IMooMedLogger _logger;

		public DnsResolutionService([NotNull] IMooMedLogger logger)
		{
			_logger = logger;
		}

		public async Task<IPAddress> ResolveDnsNameToIp(string name)
		{
			try
			{
				var hostEntry = await Dns.GetHostEntryAsync(name);

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
			return Dns.GetHostName();
		}
	}
}