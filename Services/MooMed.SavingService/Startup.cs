using Autofac;
using MooMed.AspNetCore.Grpc;
using MooMed.Caching.Module;
using MooMed.Identity.Module;
using MooMed.IPC.Module;
using MooMed.Module.Saving.Modules;

namespace MooMed.SavingService
{
	public class Startup : GrpcEndpointStartup<Service.SavingService>
	{
		protected override void RegisterModules(ContainerBuilder containerBuilder)
		{
			base.RegisterModules(containerBuilder);

			containerBuilder.RegisterModule(new CachingModule());
			containerBuilder.RegisterModule(new KubernetesModule());
			containerBuilder.RegisterModule(new IdentityModule());
			containerBuilder.RegisterModule<InternalSavingModule>();
		}
	}
}