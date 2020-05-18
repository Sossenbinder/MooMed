using Autofac;
using MooMed.AspNetCore.Grpc;
using MooMed.Caching.Module;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Core;
using MooMed.DependencyInjection.Extensions;
using MooMed.Dns.Module;
using MooMed.IPC.Module;
using MooMed.Module.Accounts.Module;
using MooMed.Stateful.AccountService.Module;
using MooMed.Stateful.AccountValidationService.Remoting;
using MooMed.Stateful.ProfilePictureService.Remoting;
using MooMed.Stateful.SessionService.Remoting;

namespace MooMed.Stateful.AccountService
{
	public class Startup : GrpcEndpointStartup<Service.AccountService>
	{
		protected override void RegisterModules(ContainerBuilder containerBuilder)
		{
			base.RegisterModules(containerBuilder);

			containerBuilder.RegisterModule<InternalAccountModule>();
			containerBuilder.RegisterModule(new CoreModule());
			containerBuilder.RegisterModule(new AccountServiceModule());
			containerBuilder.RegisterModule(new CachingModule());
			containerBuilder.RegisterModule(new KubernetesModule());
			containerBuilder.RegisterModule(new DnsModule());

			containerBuilder.RegisterGrpcService<IProfilePictureService, ProfilePictureServiceProxy>();
			containerBuilder.RegisterGrpcService<ISessionService, SessionServiceProxy>();
			containerBuilder.RegisterGrpcService<IAccountValidationService, AccountValidationServiceProxy>();
		}
	}
}