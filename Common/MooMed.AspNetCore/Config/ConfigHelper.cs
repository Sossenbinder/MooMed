using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MooMed.AspNetCore.Config.ConfigProviders;
using MooMed.DotNet.Utils.Environment;

namespace MooMed.AspNetCore.Config
{
	public static class ConfigHelper
	{
		private static readonly Dictionary<string, AbstractConfigProvider> ConfigProviders;

		static ConfigHelper()
		{
			ConfigProviders = new Dictionary<string, AbstractConfigProvider>()
			{
				{ Environments.Development, new DevelopmentConfigProvider() },
				{ Environments.Production, new ProductionConfigProvider() },
			};
		}

		public static IConfiguration CreateConfiguration(string[] args)
		{
			var environment = EnvHelper.GetDeployment();

			Console.WriteLine($"Building config for environment {environment}");

			return ConfigProviders[environment ?? throw new InvalidOperationException()].BuildConfig(args);
		}
	}
}