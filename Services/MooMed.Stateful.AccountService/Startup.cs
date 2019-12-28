using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using MooMed.AspNetCore.Grpc;
using MooMed.Stateful.AccountService.Module;

namespace MooMed.Stateful.AccountService
{
	public class Startup : GrpcEndpointStartup
	{
		protected override void RegisterServices(IEndpointRouteBuilder endpointRouteBuilder)
		{
			endpointRouteBuilder.MapGrpcService<Service.AccountService>();
		}

		protected override void RegisterModules(ContainerBuilder containerBuilder)
		{
			containerBuilder.RegisterModule(new AccountServiceModule());
		}
	}
}
