using Autofac;
using MooMed.Encryption.Interface;

namespace MooMed.Encryption.Module
{
	public class EncryptionModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);


			builder.RegisterType<SettingsCrypto>()
				.As<ISettingsCrypto>()
				.SingleInstance();

			builder.RegisterType<SettingsCryptoProvider>()
				.As<ICryptoProvider>()
				.SingleInstance();
		}
	}
}
