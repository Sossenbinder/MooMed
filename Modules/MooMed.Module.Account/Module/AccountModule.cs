using Autofac;
using MooMed.Module.Accounts.Converters;
using MooMed.Module.Accounts.Events;
using MooMed.Module.Accounts.Events.Interface;
using MooMed.Module.Accounts.Helper;
using MooMed.Module.Accounts.Helper.Interface;

namespace MooMed.Module.Accounts.Module
{
	public class AccountModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder.RegisterType<AccountEventHub>()
				.As<IAccountEventHub>()
				.SingleInstance();

			builder.RegisterType<LogonModelValidator>()
				.As<ILogonModelValidator>()
				.SingleInstance();

			builder.RegisterType<RegistrationModelValidator>()
				.As<IRegistrationModelValidator>()
				.SingleInstance();

			builder.RegisterType<FriendModelToUiModelConverter>()
				.As<FriendModelToUiModelConverter>()
				.SingleInstance();

			builder.RegisterType<AccountModelToUiModelConverter>()
				.As<AccountModelToUiModelConverter>()
				.SingleInstance();
		}
	}
}