using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MooMed.AspNetCore.Config.ConfigProviders;

namespace MooMed.AspNetCore.Config
{
	public static class ConfigHelper
	{
		private static readonly Dictionary<string, AbstractConfigProvider> _configProviders;

		static ConfigHelper()
		{
			_configProviders = new Dictionary<string, AbstractConfigProvider>()
			{
				{ Environments.Development, new DevelopmentConfigProvider() },
				{ Environments.Production, new ProductionConfigProvider() },
			};
		}

		public static IConfiguration CreateConfiguration(string[] args)
		{
			var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

			return _configProviders[environment ?? throw new InvalidOperationException()].BuildConfig(args);
		}
	}
}
