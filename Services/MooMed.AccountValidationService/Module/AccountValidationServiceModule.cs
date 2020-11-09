using Autofac;
using MooMed.RemotingProxies.Proxies;
using MooMed.ServiceBase.Services.Interface;

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