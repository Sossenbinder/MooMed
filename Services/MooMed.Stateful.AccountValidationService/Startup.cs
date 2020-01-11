using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using MooMed.AspNetCore.Grpc;

namespace MooMed.Stateful.AccountValidationService
{
	public class Startup : GrpcEndpointStartup
	{
		protected override void RegisterServices(IEndpointRouteBuilder endpointRouteBuilder)
		{
			endpointRouteBuilder.MapGrpcService<Service.AccountValidationService>();
		}
	}
}
