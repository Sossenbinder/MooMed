using System;
using System.IO;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MooMed.AspNetCore.Config;
using MooMed.AspNetCore.Extensions;

namespace MooMed.AspNetCore.Helper
{
	public static class MooMedHostBuilder
	{
		public static IHost BuildDefaultKestrelHost<TStartup>(
			string[] args, 
			Action<IHostBuilder>? hostBuilderEnricher = null,
			Action<IWebHostBuilder>? webHostBuilderEnricher = null) 
			where TStartup : class
		{
			var hostBuilder = CreateSharedHost<TStartup>(args, webHostBuilder =>
			{
				webHostBuilder.UseContentRoot(Directory.GetCurrentDirectory());

				webHostBuilderEnricher?.Invoke(webHostBuilder);
			});

			hostBuilderEnricher?.Invoke(hostBuilder);

			return hostBuilder.Build();
		}

		public static IHost BuildDefaultGrpcServiceHost<TStartup>(
			string[] args)
			where TStartup : class
		{
			var hostBuilder = CreateSharedHost<TStartup>(args, webHostBuilder =>
			{
				webHostBuilder.ConfigureGrpc();
			});

			return hostBuilder.Build();
		}

		private static IHostBuilder CreateSharedHost<TStartup>(
			string[] args,
			Action<IWebHostBuilder> webHostBuilderEnricher = null)
			where TStartup : class
		{
			return Host
				.CreateDefaultBuilder(args)
				.UseServiceProviderFactory(new AutofacServiceProviderFactory())
				.ConfigureWebHostDefaults(webHostBuilder =>
				{
					webHostBuilder
						.UseStartup<TStartup>()
						.UseConfiguration(ConfigHelper.CreateConfiguration(args));

					webHostBuilder.UseSentry();

					webHostBuilderEnricher(webHostBuilder);
				});
		}
	}
}
