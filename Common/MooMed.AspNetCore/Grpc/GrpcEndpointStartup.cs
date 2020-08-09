﻿using System.Reflection;
using Autofac;
using JetBrains.Annotations;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using MooMed.AspNetCore.Modules;
using MooMed.Common.ServiceBase.Module;
using MooMed.Configuration.Module;
using MooMed.Core;
using MooMed.Encryption.Module;
using MooMed.Eventing.Helper;
using MooMed.Eventing.Module;
using MooMed.Grpc.Definitions.Interface;
using MooMed.Grpc.Interceptors;
using MooMed.Logging.Module;
using ProtoBuf.Grpc.Server;

namespace MooMed.AspNetCore.Grpc
{
	public abstract class GrpcEndpointStartup<TGrpcService>
		where TGrpcService : class, IGrpcService
	{
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public virtual void ConfigureServices(IServiceCollection services)
		{
			services.AddCodeFirstGrpc(grpcOptions =>
			{
				grpcOptions.EnableDetailedErrors = true;
				grpcOptions.Interceptors.Add<ErrorHandlingInterceptor>();
			});

			services.AddLocalization();
			services.AddControllers()
				.AddApplicationPart(Assembly.GetExecutingAssembly())
				.AddControllersAsServices();

			services.AddDataProtection();

			services.AddMassTransit(x => x.AddBus(provider => MassTransitBusFactory.CreateBus(provider)));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseRouting();
			app.UseEndpoints(RegisterServices);
		}

		// Autofac entry point
		public void ConfigureContainer(ContainerBuilder containerBuilder) => RegisterModules(containerBuilder);

		protected virtual void RegisterModules(ContainerBuilder containerBuilder)
		{
			containerBuilder.RegisterType<TGrpcService>()
				.As<IGrpcService, TGrpcService>()
				.SingleInstance();

			containerBuilder.RegisterModule<MooMedAspNetCoreModule>();
			containerBuilder.RegisterModule<EventingModule>();
			containerBuilder.RegisterModule<CoreModule>();
			containerBuilder.RegisterModule<EncryptionModule>();
			containerBuilder.RegisterModule<ConfigurationModule>();
			containerBuilder.RegisterModule<LoggingModule>();
			containerBuilder.RegisterModule<ServiceBaseModule>();
		}

		protected void RegisterServices([NotNull] IEndpointRouteBuilder endpointRouteBuilder)
		{
			endpointRouteBuilder.MapGrpcService<TGrpcService>();
			endpointRouteBuilder.MapControllers();
		}
	}
}