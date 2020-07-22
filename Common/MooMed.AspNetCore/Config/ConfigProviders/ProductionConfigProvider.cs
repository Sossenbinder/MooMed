using System;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace MooMed.AspNetCore.Config.ConfigProviders
{
	internal class ProductionConfigProvider : AbstractConfigProvider
	{
		public ProductionConfigProvider() 
			: base(Environments.Production)
		{
		}

		public override IConfiguration BuildConfig(string[] args)
		{
			Environment.SetEnvironmentVariable("SENTRY_SERVERNAME", Assembly.GetExecutingAssembly().FullName);

			ConfigBuilder.AddJsonFile("Config/sentryconfig.json", true, true);

			return base.BuildConfig(args);
		}
	}
}
