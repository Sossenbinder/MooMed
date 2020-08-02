using Autofac;
using MooMed.Module.Accounts.Helper;
using MooMed.Module.Accounts.Helper.Interface;
using MooMed.Module.AccountValidation.Converters;

namespace MooMed.Module.AccountValidation.Module
{
	public class AccountValidationModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<AccountValidationUiModelConverter>()
				.As<AccountValidationUiModelConverter>()
				.SingleInstance();

			base.Load(builder);
		}
	}
}