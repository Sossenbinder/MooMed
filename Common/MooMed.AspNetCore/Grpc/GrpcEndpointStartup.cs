using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
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
using MooMed.Grpc.Interceptors;
using MooMed.Logging.Module;
using MooMed.Module.Monitoring.Eventing;
using MooMed.Module.Monitoring.Eventing.Interface;
using MooMed.Module.Monitoring.Module;
using MooMed.ServiceBase.Definitions.Interface;
using ProtoBuf.Grpc.Server;

namespace MooMed.AspNetCore.Grpc
{
	public abstract class GrpcEndpointStartup<TGrpcService>
		where TGrpcService : class, IGrpcService
	{
		#region Pipeline

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseRouting();
			app.UseEndpoints(RegisterEndpoints);
		}

		protected void RegisterEndpoints([NotNull] IEndpointRouteBuilder endpointRouteBuilder)
		{
			endpointRouteBuilder.MapGrpcService<TGrpcService>();
			endpointRouteBuilder.MapControllers();
		}

		#endregion Pipeline

		#region Dependency Injection

		// Regular .net core DI entry point
		public virtual void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<IMonitoringEventHub, MonitoringEventHub>();

			services.AddCodeFirstGrpc(grpcOptions =>
			{
				grpcOptions.EnableDetailedErrors = true;
				grpcOptions.Interceptors.Add<MetricsCounterInterceptor>();
				grpcOptions.Interceptors.Add<ServiceResponseConversionInterceptor>();
			});

			services.AddLocalization();
			services.AddControllers()
				.AddApplicationPart(Assembly.GetExecutingAssembly())
				.AddControllersAsServices();

			services.AddDataProtection();

			services.AddMassTransit(x => x.AddBus(provider => MassTransitBusFactory.CreateBus(provider)));
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
			containerBuilder.RegisterModule<ConfigurationModule>();
			containerBuilder.RegisterModule<CoreModule>();
			containerBuilder.RegisterModule<EncryptionModule>();
			containerBuilder.RegisterModule<LoggingModule>();
			containerBuilder.RegisterModule<ServiceBaseModule>();
			containerBuilder.RegisterModule<MonitoringModule>();
		}

		#endregion Dependency Injection
	}
}