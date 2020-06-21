using System;
using System.IO;
using System.Reflection;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
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
				webHostBuilder
					.UseContentRoot(Directory.GetCurrentDirectory())
					.UseKestrel();

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
				webHostBuilder
					.ConfigureGrpc();
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
						.UseConfiguration(CreateConfig(args))
						.UseStartup<TStartup>();

					webHostBuilderEnricher(webHostBuilder);
				});
		}

		private static IConfiguration CreateConfig([System.Diagnostics.CodeAnalysis.NotNull] string[] args)
		{
			var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

			var configBuilder = new ConfigurationBuilder()
				.AddEnvironmentVariables()
				.AddJsonFile("appsettings.json", true, true)
				.AddJsonFile($"appsettings.{environment}.json", true, true);
			
			if (args != null)
			{
				configBuilder.AddCommandLine(args);
			}

			if (environment?.Equals(Environments.Development) ?? false)
			{
				var appAssembly = Assembly.GetEntryAssembly();
				if (appAssembly != null)
				{
					configBuilder.AddUserSecrets(appAssembly);
				}
			}

			var intermediateBuild = configBuilder.Build();

			AddKeyVault(configBuilder, intermediateBuild);

			return configBuilder.Build();
		}

		private static void AddKeyVault([System.Diagnostics.CodeAnalysis.NotNull] IConfigurationBuilder configBuilder, [System.Diagnostics.CodeAnalysis.NotNull] IConfiguration config)
		{
			var keyVaultEndpoint = "https://moomed.vault.azure.net/";

			var keyVaultClient = config["AZURE_CLIENT_ID"];

			var keyVaultSecret = config["AZURE_CLIENT_SECRET"];

			configBuilder.AddAzureKeyVault(keyVaultEndpoint, keyVaultClient, keyVaultSecret);
		}
	}
}
