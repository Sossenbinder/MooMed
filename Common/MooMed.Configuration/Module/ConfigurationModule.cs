using Autofac;
using MooMed.Common.Definitions.Configuration;
using MooMed.Configuration.Interface;

namespace MooMed.Configuration.Module
{
	public class ConfigurationModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder.RegisterType<Config>()
				.As<IConfig>()
				.SingleInstance();

			builder.RegisterType<MainConfigSettingsProvider>()
				.As<IConfigSettingsProvider>()
				.SingleInstance();
		}
	}
}
