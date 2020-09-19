using Autofac;
using MooMed.ServiceBase.Services.Interface;
using MooMed.AccountValidationService.Remoting;

namespace MooMed.AccountValidationService.Module
{
	public class AccountValidationServiceModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder.RegisterType<AccountValidationServiceProxy>()
				.As<IAccountValidationService>()
				.SingleInstance();
		}
	}
}