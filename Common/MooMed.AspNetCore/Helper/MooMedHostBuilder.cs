﻿using System;
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
			return CreateSharedHost<TStartup>(
				args,
				hostBuilderEnricher,
				webHostBuilder =>
				{
					webHostBuilder.UseContentRoot(Directory.GetCurrentDirectory());

					webHostBuilderEnricher?.Invoke(webHostBuilder);
				}
			);
		}

		public static IHost BuildDefaultGrpcServiceHost<TStartup>(
			string[] args,
			Action<IHostBuilder>? hostBuilderEnricher = null,
			Action<IWebHostBuilder>? webHostBuilderEnricher = null)
			where TStartup : class
		{
			return CreateSharedHost<TStartup>(
				args,
				hostBuilderEnricher,
				webHostBuilder =>
				{
					webHostBuilder.ConfigureGrpc();

					webHostBuilderEnricher?.Invoke(webHostBuilder);
				});
		}

		private static IHost CreateSharedHost<TStartup>(
			string[] args,
			Action<IHostBuilder>? hostBuilderEnricher = null,
			Action<IWebHostBuilder> webHostBuilderEnricher = null)
			where TStartup : class
		{
			var hostBuilder = Host
				.CreateDefaultBuilder(args)
				.UseServiceProviderFactory(new AutofacServiceProviderFactory())
				.ConfigureWebHostDefaults(webHostBuilder =>
				{
					webHostBuilder
						.UseStartup<TStartup>()
						.UseConfiguration(ConfigHelper.CreateConfiguration(args))
						.AddAppMetricsWithPrometheusSupport();

					webHostBuilder.UseSentry();

					webHostBuilderEnricher(webHostBuilder);
				}
			);

			hostBuilderEnricher?.Invoke(hostBuilder);

			return hostBuilder.Build();
		}
	}
}