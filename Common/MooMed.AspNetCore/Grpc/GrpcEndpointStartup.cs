using Autofac;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using MooMed.AspNetCore.Modules;
using MooMed.Grpc.Definitions.Interface;
using ProtoBuf.Grpc.Server;

namespace MooMed.AspNetCore.Grpc
{
	public abstract class GrpcEndpointStartup<T>
		where T : class, IGrpcService
	{
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public virtual void ConfigureServices(IServiceCollection services)
		{
			services.AddCodeFirstGrpc();
			services.AddLocalization();

			services.AddSingleton<T>();
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
			containerBuilder.RegisterModule(new GrpcModule());
		}

		protected abstract void RegisterServices([NotNull] IEndpointRouteBuilder endpointRouteBuilder);
	}
}
