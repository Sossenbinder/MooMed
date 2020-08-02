using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MooMed.Configuration.Module;
using MooMed.Core;
using MooMed.Identity.Module;
using MooMed.Encryption.Module;
using MooMed.Eventing.Module;
using MooMed.Logging.Module;

namespace KubeWatcher
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Booting Kubewatcher...");

			var host = Host.CreateDefaultBuilder(args)
				.UseServiceProviderFactory(new AutofacServiceProviderFactory())
				.ConfigureContainer<ContainerBuilder>(builder => {

					builder.RegisterModule<CoreModule>();
					builder.RegisterModule<EncryptionModule>();
					builder.RegisterModule<ConfigurationModule>();
					builder.RegisterModule<LoggingModule>();
					builder.RegisterModule<IdentityModule>();
					builder.RegisterModule<EventingModule>();
				})
				.ConfigureServices((hostContext, services) =>
				{
					services.AddHostedService<Worker>();
				})
				.Build();

			host.Run();
		}
	}
}
