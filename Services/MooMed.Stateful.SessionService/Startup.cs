using Autofac;
using MooMed.AspNetCore.Grpc;
using MooMed.Caching.Module;
using MooMed.Identity.Module;
using MooMed.IPC.Module;
using MooMed.Module.Accounts.Module;
using MooMed.Stateful.SessionService.Module;

namespace MooMed.Stateful.SessionService
{
	public class Startup : GrpcEndpointStartup<Service.SessionService>
	{
		protected override void RegisterModules(ContainerBuilder containerBuilder)
		{
			base.RegisterModules(containerBuilder);

			containerBuilder.RegisterModule(new SessionServiceModule());
			containerBuilder.RegisterModule(new CachingModule());
			containerBuilder.RegisterModule(new KubernetesModule());
			containerBuilder.RegisterModule(new IdentityModule());
			containerBuilder.RegisterModule(new AccountModule());
		}
	}
}