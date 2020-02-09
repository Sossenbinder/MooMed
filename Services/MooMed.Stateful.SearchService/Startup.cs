using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using MooMed.AspNetCore.Grpc;

namespace MooMed.Stateful.SearchService
{
	public class Startup : GrpcEndpointStartup<Service.SearchService>
	{
		protected override void RegisterServices(IEndpointRouteBuilder endpointRouteBuilder)
		{
			endpointRouteBuilder.MapGrpcService<Service.SearchService>();
		}
	}
}
