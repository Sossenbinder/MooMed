using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Hosting;
using MooMed.AspNetCore.Extensions;

namespace MooMed.AspNetCore.Helper
{
	public static class MooMedHostBuilder
	{
		public static IHost BuildDefaultKestrelHost<TStartUp>([NotNull] string[] args) 
			where TStartUp : class
		{
			return Host
				.CreateDefaultBuilder(args)
				.UseServiceProviderFactory(new AutofacServiceProviderFactory())
				.ConfigureWebHostDefaults(webHostBuilder => {
					webHostBuilder
						.UseConfiguration(CreateConfig(args))
						.UseContentRoot(Directory.GetCurrentDirectory())
						.UseKestrel()
						.UseStartup<TStartUp>();
				})
				.Build();
		}

		public static IHost BuildDefaultGrpcServiceHost<TStartUp>([NotNull] string[] args) 
			where TStartUp : class
		{
			return Host
				.CreateDefaultBuilder(args)
				.UseServiceProviderFactory(new AutofacServiceProviderFactory())
				.ConfigureWebHostDefaults(webHostBuilder => {
					webHostBuilder
						.UseConfiguration(CreateConfig(args))
						.ConfigureGrpc()
						.UseStartup<TStartUp>();
				})
				.Build();
		}

		private static IConfiguration CreateConfig([NotNull] string[] args)
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

		private static void AddKeyVault([NotNull] IConfigurationBuilder configBuilder, [NotNull] IConfiguration config)
		{
			var keyVaultEndpoint = "https://moomed.vault.azure.net/";

			var keyVaultClient = config["AZURE_CLIENT_ID"];

			var keyVaultSecret = config["AZURE_CLIENT_SECRET"];

			configBuilder.AddAzureKeyVault(keyVaultEndpoint, keyVaultClient, keyVaultSecret);
		}
	}
}
