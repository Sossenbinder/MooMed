using System.IO;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace MooMed.Web
{
	internal static class Program
	{
		public static void Main(string[] args)
		{
			var host = Host.CreateDefaultBuilder(args)
				.UseServiceProviderFactory(new AutofacServiceProviderFactory())
				.ConfigureAppConfiguration(configBuilder =>
				{
					configBuilder.AddEnvironmentVariables();
				})
				.ConfigureWebHostDefaults(webHostBuilder => {
					webHostBuilder
						.UseContentRoot(Directory.GetCurrentDirectory())
						.UseKestrel()
						.UseStartup<Startup.Startup>();
				})
				.Build();

			host.Run();
		}
	}
}
