using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MooMed.AspNetCore.Extensions;

namespace MooMed.Stateful.AccountValidationService
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = Host.CreateDefaultBuilder(args)
				.UseServiceProviderFactory(new AutofacServiceProviderFactory())
				.ConfigureWebHostDefaults(webHostBuilder =>
				{
					webHostBuilder
						.ConfigureGrpc()
						.UseStartup<Startup>();
				})
				.Build();

			host.Run();
		}
	}
}
