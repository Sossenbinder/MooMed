using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using MooMed.AspNetCore.Helper;
using MooMed.Frontend.StartupConfigs;

namespace MooMed.Frontend
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var host = MooMedHostBuilder.BuildDefaultKestrelHost<Startup>(args);

			await host.RunAsync();
		}
	}
}