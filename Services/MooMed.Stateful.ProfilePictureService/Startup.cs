using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using MooMed.AspNetCore.Grpc;
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
		}
	}
}
