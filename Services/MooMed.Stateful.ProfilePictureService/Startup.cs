using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using MooMed.AspNetCore.Grpc;
using MooMed.Caching.Module;
using MooMed.Core;
using MooMed.IPC.Module;
using MooMed.Stateful.ProfilePictureService.Module;

namespace MooMed.Stateful.ProfilePictureService
{
	public class Startup : GrpcEndpointStartup
	{
		protected override void RegisterServices(IEndpointRouteBuilder endpointRouteBuilder)
		{
			endpointRouteBuilder.MapGrpcService<Service.ProfilePictureService>();
		}

		protected override void RegisterModules(ContainerBuilder containerBuilder)
		{
			containerBuilder.RegisterModule(new ProfilePictureModule());
			containerBuilder.RegisterModule(new CoreBindings());
			containerBuilder.RegisterModule(new CachingModule());
			containerBuilder.RegisterModule(new KubernetesModule());
		}
	}
}
