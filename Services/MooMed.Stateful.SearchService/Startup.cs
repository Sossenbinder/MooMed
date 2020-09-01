using Autofac;
using MooMed.AspNetCore.Grpc;
using MooMed.Caching.Module;
using MooMed.DependencyInjection.Extensions;
using MooMed.Identity.Module;
using MooMed.IPC.Module;
using MooMed.ServiceBase.Services.Interface;
using MooMed.Stateful.AccountService.Remoting;
using MooMed.Stateful.SearchService.Module;

namespace MooMed.Stateful.SearchService
{
	public class Startup : GrpcEndpointStartup<Service.SearchService>
	{
		protected override void RegisterModules(ContainerBuilder containerBuilder)
		{
			base.RegisterModules(containerBuilder);

			containerBuilder.RegisterModule(new CachingModule());
			containerBuilder.RegisterModule(new KubernetesModule());
			containerBuilder.RegisterModule(new IdentityModule());
			containerBuilder.RegisterModule(new SearchModule());

			containerBuilder.RegisterGrpcService<IAccountService, AccountServiceProxy>();
		}
	}
}