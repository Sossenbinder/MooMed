using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using MooMed.AspNetCore.Grpc;
using MooMed.Caching.Module;
using MooMed.Core;
using MooMed.Dns.Module;
using MooMed.IPC.Module;
using MooMed.Module.Accounts;
using MooMed.Module.Accounts.Module;
using MooMed.Stateful.AccountValidationService.Module;

namespace MooMed.Stateful.AccountValidationService
{
	public class Startup : GrpcEndpointStartup<Service.AccountValidationService>
	{
		protected override void RegisterServices(IEndpointRouteBuilder endpointRouteBuilder)
		{
			endpointRouteBuilder.MapGrpcService<Service.AccountValidationService>();
		}

		protected override void RegisterModules(ContainerBuilder containerBuilder)
		{
			base.RegisterModules(containerBuilder);

			containerBuilder.RegisterModule(new AccountModule());
			containerBuilder.RegisterModule(new CoreModule());
			containerBuilder.RegisterModule(new AccountValidationServiceModule());
			containerBuilder.RegisterModule(new CachingModule());
			containerBuilder.RegisterModule(new KubernetesModule());
			containerBuilder.RegisterModule(new DnsModule());
		}
	}
}
