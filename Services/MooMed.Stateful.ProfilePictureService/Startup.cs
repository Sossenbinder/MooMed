using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using MooMed.AspNetCore.Grpc;
using MooMed.AspNetCore.Modules;
using MooMed.Caching.Module;
using MooMed.Core;
using MooMed.IPC.Module;
using MooMed.Stateful.ProfilePictureService.Module;

namespace MooMed.Stateful.ProfilePictureService
{
	public class Startup : GrpcEndpointStartup<Service.ProfilePictureService>
	{
		protected override void RegisterServices(IEndpointRouteBuilder endpointRouteBuilder)
		{
			endpointRouteBuilder.MapGrpcService<Service.ProfilePictureService>();
		}

		protected override void RegisterModules(ContainerBuilder containerBuilder)
		{
			base.RegisterModules(containerBuilder);

			containerBuilder.RegisterModule(new ProfilePictureModule());
			containerBuilder.RegisterModule(new CoreModule());
			containerBuilder.RegisterModule(new CachingModule());
			containerBuilder.RegisterModule(new KubernetesModule());
		}
	}
}
