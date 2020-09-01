using Autofac;
using MooMed.AspNetCore.Grpc;
using MooMed.Caching.Module;
using MooMed.Identity.Module;
using MooMed.IPC.Module;
using MooMed.Stateful.ProfilePictureService.Module;

namespace MooMed.Stateful.ProfilePictureService
{
	public class Startup : GrpcEndpointStartup<Service.ProfilePictureService>
	{
		protected override void RegisterModules(ContainerBuilder containerBuilder)
		{
			base.RegisterModules(containerBuilder);

			containerBuilder.RegisterModule(new ProfilePictureModule());
			containerBuilder.RegisterModule(new CachingModule());
			containerBuilder.RegisterModule(new KubernetesModule());
			containerBuilder.RegisterModule(new IdentityModule());
		}
	}
}