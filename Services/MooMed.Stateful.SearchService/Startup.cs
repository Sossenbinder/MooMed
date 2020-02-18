using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using MooMed.AspNetCore.Grpc;
using MooMed.Caching.Module;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Core;
using MooMed.IPC.Module;
using MooMed.Stateful.AccountService.Remoting;
using MooMed.Stateful.SearchService.Module;

namespace MooMed.Stateful.SearchService
{
	public class Startup : GrpcEndpointStartup<Service.SearchService>
	{
		protected override void RegisterServices(IEndpointRouteBuilder endpointRouteBuilder)
		{
			endpointRouteBuilder.MapGrpcService<Service.SearchService>();
		}

		protected override void RegisterModules(ContainerBuilder containerBuilder)
		{
			base.RegisterModules(containerBuilder);

			containerBuilder.RegisterModule(new CoreModule());
			containerBuilder.RegisterModule(new CachingModule());
			containerBuilder.RegisterModule(new KubernetesModule());
			containerBuilder.RegisterModule(new SearchModule());

			containerBuilder.RegisterType<AccountServiceProxy>()
				.As<IAccountService>()
				.SingleInstance();
		}
	}
}
