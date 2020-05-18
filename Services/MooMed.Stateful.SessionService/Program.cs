using Microsoft.Extensions.Hosting;
using MooMed.AspNetCore.Helper;

namespace MooMed.Stateful.SessionService
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = MooMedHostBuilder.BuildDefaultGrpcServiceHost<Startup>(args);

			host.Run();
		}
	}
}
