using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using MooMed.AspNetCore.Helper;

namespace MooMed.ChatService
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var host = MooMedHostBuilder.BuildDefaultGrpcServiceHost<Startup>(args);

			await host.RunAsync();
		}
	}
}