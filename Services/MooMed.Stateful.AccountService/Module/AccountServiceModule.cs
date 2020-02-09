using Autofac;
using MooMed.AspNetCore.Modules;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Stateful.ProfilePictureService.Remoting;
using MooMed.Stateful.SessionService.Remoting;

namespace MooMed.Stateful.AccountService.Module
{
	public class AccountServiceModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder.RegisterType<ProfilePictureServiceProxy>()
				.As<IProfilePictureService>()
				.SingleInstance();

			builder.RegisterType<SessionServiceProxy>()
				.As<ISessionService>()
				.SingleInstance();
		}
	}
}
