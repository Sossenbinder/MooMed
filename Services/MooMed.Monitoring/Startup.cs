using Autofac;
using MooMed.AspNetCore.Grpc;
using MooMed.Caching.Module;
using MooMed.Core;
using MooMed.Dns.Module;
using MooMed.IPC.Module;
using MooMed.Module.Monitoring.Module;
using MooMed.Module.Monitoring.Service;

namespace MooMed.Monitoring
{
	public class Startup : GrpcEndpointStartup<MonitoringService>
	{
		protected override void RegisterModules(ContainerBuilder containerBuilder)
		{
			base.RegisterModules(containerBuilder);

			containerBuilder.RegisterModule(new CoreModule());
			containerBuilder.RegisterModule(new CachingModule());
			containerBuilder.RegisterModule(new KubernetesModule());
			containerBuilder.RegisterModule(new DnsModule());
			containerBuilder.RegisterModule<MonitoringModule>();
		}
	}
}
