using Autofac;
using Microsoft.Extensions.DependencyInjection;
using MooMed.AspNetCore.Grpc;
using MooMed.Caching.Module;
using MooMed.Core;
using MooMed.Dns.Module;
using MooMed.FinanceService.Module;
using MooMed.IPC.Module;

namespace MooMed.FinanceService
{
	public class Startup : GrpcEndpointStartup<Service.FinanceService>
	{
		public override void ConfigureServices(IServiceCollection services)
		{
			base.ConfigureServices(services);

			services.AddHttpClient();
		}
		
		protected override void RegisterModules(ContainerBuilder containerBuilder)
		{
			base.RegisterModules(containerBuilder);

			containerBuilder.RegisterModule(new CoreModule());
			containerBuilder.RegisterModule(new CachingModule());
			containerBuilder.RegisterModule(new KubernetesModule());
			containerBuilder.RegisterModule(new DnsModule());
			containerBuilder.RegisterModule(new FinanceServiceModule());
		}
	}
}