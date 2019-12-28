using Autofac;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Stateful.AccountValidationService.Remoting;

namespace MooMed.Stateful.AccountValidationService.Module
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
