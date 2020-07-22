using System;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace MooMed.AspNetCore.Config.ConfigProviders
{
	internal class DevelopmentConfigProvider : AbstractConfigProvider
	{
		public DevelopmentConfigProvider()
			:base(Environments.Development)
		{

		}

		public override IConfiguration BuildConfig(string[] args)
		{
			var appAssembly = Assembly.GetEntryAssembly();
			if (appAssembly != null)
			{
				ConfigBuilder.AddUserSecrets(appAssembly);
			}

			return base.BuildConfig(args);
		}
	}
}
