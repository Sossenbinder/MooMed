using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MooMed.Core;
using MooMed.Dns.Module;
using MooMed.Eventing.Module;

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
					builder.RegisterModule<DnsModule>();
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
