using System.Threading.Tasks;
using MooMed.AspNetCore.Helper;

namespace MooMed.Monitoring
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var host = MooMedHostBuilder.BuildDefaultKestrelHost<Startup>(args);

			await host.StartAsync();
		}
	}
}