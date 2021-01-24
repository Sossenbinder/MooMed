using Microsoft.Extensions.Configuration;
using MooMed.Logging.Loggers;

namespace MooMed.AspNetCore.Config.ConfigProviders
{
	internal abstract class AbstractConfigProvider
	{
		protected IConfigurationBuilder ConfigBuilder;

		private readonly string _environment;

		protected AbstractConfigProvider(string environment)
		{
			_environment = environment;

			ConfigBuilder = new ConfigurationBuilder();
		}

		public virtual IConfiguration BuildConfig(string[] args)
		{
			ConfigBuilder
				.AddEnvironmentVariables()
				.AddJsonFile("appsettings.json", true, true)
				.AddJsonFile($"appsettings.{_environment}.json", true, true)
				.AddInMemoryCollection(LoggerConstants.GetConstantsAsInMemoryDict());

			if (args != null)
			{
				ConfigBuilder.AddCommandLine(args);
			}

			var intermediateConfig = ConfigBuilder.Build();

			var finalBuilder = AddKeyVault(ConfigBuilder, intermediateConfig);

			return finalBuilder.Build();
		}

		private static IConfigurationBuilder AddKeyVault(IConfigurationBuilder configBuilder, IConfiguration config)
		{
			const string? keyVaultEndpoint = "https://moomed.vault.azure.net/";

			var keyVaultClient = config["AZURE_CLIENT_ID"];

			var keyVaultSecret = config["AZURE_CLIENT_SECRET"];

			if (keyVaultSecret != null && keyVaultClient != null)
			{
				return configBuilder.AddAzureKeyVault(keyVaultEndpoint, keyVaultClient, keyVaultSecret);
			}

			return configBuilder;
		}
	}
}