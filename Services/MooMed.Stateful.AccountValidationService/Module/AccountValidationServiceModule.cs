using Autofac;
using MooMed.Grpc.Services.Interface;
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
