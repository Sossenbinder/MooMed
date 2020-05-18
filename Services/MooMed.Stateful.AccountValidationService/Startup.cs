using Autofac;
using MooMed.AspNetCore.Grpc;
using MooMed.Caching.Module;
using MooMed.Core;
using MooMed.Dns.Module;
using MooMed.IPC.Module;
using MooMed.Module.Accounts.Module;
using MooMed.Stateful.AccountValidationService.Module;

namespace MooMed.Stateful.AccountValidationService
{
	public class Startup : GrpcEndpointStartup<Service.AccountValidationService>
	{
		protected override void RegisterModules(ContainerBuilder containerBuilder)
		{
			base.RegisterModules(containerBuilder);

			containerBuilder.RegisterModule<AccountValidationModule>();
			containerBuilder.RegisterModule<CoreModule>();
			containerBuilder.RegisterModule<AccountValidationServiceModule>();
			containerBuilder.RegisterModule<CachingModule>();
			containerBuilder.RegisterModule<KubernetesModule>();
			containerBuilder.RegisterModule<DnsModule>();
		}
	}
}
