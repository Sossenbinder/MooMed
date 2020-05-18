using Microsoft.Extensions.Hosting;
using MooMed.AspNetCore.Helper;

namespace MooMed.Web
{
	internal static class Program
	{
		public static void Main(string[] args)
		{
			var host = MooMedHostBuilder.BuildDefaultKestrelHost<Startup.Startup>(args);

			host.Run();
		}
	}
}
