using System.Threading.Tasks;
using App.Metrics.AspNetCore;
using MooMed.AspNetCore.Extensions;
using MooMed.AspNetCore.Helper;

namespace MooMed.Monitoring
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var host = MooMedHostBuilder.BuildDefaultKestrelHost<Startup>(
				args, 
				webHostBuilderEnricher: webHostBuilder => webHostBuilder
						.ConfigureGrpc()
						.UseMetrics());

			await host.StartAsync();
		}
	}
}
