using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MooMed.AspNetCore.Grpc;
using MooMed.Caching.Module;
using MooMed.Core;
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

			containerBuilder.RegisterModule(new CachingModule());
			containerBuilder.RegisterModule(new KubernetesModule());
			containerBuilder.RegisterModule(new IdentityModule());
			containerBuilder.RegisterModule<MonitoringModule>();
			containerBuilder.RegisterModule<Module.MonitoringModule>();
		}
	}
}
