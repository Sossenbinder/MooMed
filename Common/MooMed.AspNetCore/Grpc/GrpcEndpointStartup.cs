using Autofac;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc.Server;

namespace MooMed.AspNetCore.Grpc
{
	public abstract class GrpcEndpointStartup
	{
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCodeFirstGrpc();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseRouting();
			app.UseEndpoints(RegisterServices);
		}

		public void ConfigureContainer([NotNull] ContainerBuilder containerBuilder) => RegisterModules(containerBuilder);

		protected virtual void RegisterModules([NotNull] ContainerBuilder containerBuilder)
		{

		}

		protected abstract void RegisterServices([NotNull] IEndpointRouteBuilder endpointRouteBuilder);
	}
}
