using System;
using Autofac;
using JetBrains.Annotations;
using MassTransit;
using MassTransit.SignalR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MooMed.Caching.Module;
using MooMed.Core;
using MooMed.Core.Code.Logging.Loggers.Interface;
using MooMed.Dns.Module;
using MooMed.Dns.Service.Interface;
using MooMed.Eventing.Helper;
using MooMed.Eventing.Module;
using MooMed.IPC.Module;
using MooMed.SignalR.Hubs;
using MooMed.Web.Modules;

namespace MooMed.Web.Startup
{
    public class Startup
    {
	    // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
	        try
	        {
		        ConfigureMassTransit(services);

		        services.AddMvc();
		        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => options.LoginPath = "/Logon/Login");
			}
	        catch (DllNotFoundException)
	        {
				// TODO: Figure out why this is being raised
	        }
        }

        private void ConfigureMassTransit([NotNull] IServiceCollection services)
        {
	        services.AddSignalR().AddMassTransitBackplane();

	        // creating the bus config
	        services.AddMassTransit(x =>
	        {
		        // Add this for each Hub you have
		        x.AddSignalRHubConsumers<ChatHub>();
		        x.AddSignalRHubConsumers<NotificationHub>();

		        x.AddBus(provider => MassTransitBusFactory.CreateBus(provider, cfg =>
		        {
			        cfg.AddSignalRHubEndpoints<ChatHub>(provider);
			        cfg.AddSignalRHubEndpoints<NotificationHub>(provider);
		        }));
	        });
		}

        public void ConfigureContainer([NotNull] ContainerBuilder builder)
        {
            builder.RegisterModule(new CoreModule());
            builder.RegisterModule(new CachingModule());
            builder.RegisterModule(new WebGrpcModule());
            builder.RegisterModule(new KubernetesModule());
            builder.RegisterModule(new DnsModule());
			builder.RegisterModule(new EventingModule());
        }

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure([NotNull] IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseDefaultFiles();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpointRouteBuilder =>
			{
				RouteConfig.RegisterRoutes(endpointRouteBuilder);

				endpointRouteBuilder.MapHub<ChatHub>("/chatHub");
				endpointRouteBuilder.MapHub<NotificationHub>("/notificationHub");
			});
		}
    }
}
