using Autofac;
using Microsoft.Extensions.DependencyInjection;
using MooMed.AspNetCore.Grpc;
using MooMed.Caching.Module;
using MooMed.Identity.Module;
using MooMed.IPC.Module;
using MooMed.Module.Monitoring.Module;

namespace MooMed.Monitoring
{
	public class Startup : GrpcEndpointStartup<Service.MonitoringService>
	{
		protected override void RegisterModules(ContainerBuilder containerBuilder)
		{
			base.RegisterModules(containerBuilder);

			containerBuilder.RegisterModule<CachingModule>();
			containerBuilder.RegisterModule<KubernetesModule>();
			containerBuilder.RegisterModule<IdentityModule>();
			containerBuilder.RegisterModule<MonitoringModule>();
		}
	}
}