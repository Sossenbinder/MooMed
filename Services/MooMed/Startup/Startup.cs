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
using MooMed.Dns.Module;
using MooMed.Eventing.Helper;
using MooMed.Eventing.Module;
using MooMed.IPC.Module;
using MooMed.Module.Finance.Modules;
using MooMed.SignalR.Hubs;
using MooMed.Web.Modules;

namespace MooMed.Web.Startup
{
    public class Startup
    {
	    // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
	        ConfigureMassTransit(services);

	        services
				.AddMvc();

	        services
		        .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
		        .AddCookie(options => options.LoginPath = "/Logon/Login");

	        services.AddAntiforgery(x => x.HeaderName = "AntiForgery");
        }

        private void ConfigureMassTransit([NotNull] IServiceCollection services)
        {
	        services.AddSignalR().AddMassTransitBackplane();

	        // creating the bus config
	        services.AddMassTransit(x =>
	        {
		        // Add this for each Hub you have
		        x.AddSignalRHubConsumers<SignalRHub>();

		        x.AddBus(provider => MassTransitBusFactory.CreateBus(provider, cfg =>
		        {
			        cfg.AddSignalRHubEndpoints<SignalRHub>(provider);
		        }));
	        });
		}

        [UsedImplicitly]
        public void ConfigureContainer([NotNull] ContainerBuilder builder)
        {
			builder.RegisterModule<CoreModule>();
			builder.RegisterModule<CachingModule>();
			builder.RegisterModule<WebGrpcModule>();
			builder.RegisterModule<KubernetesModule>();
			builder.RegisterModule<DnsModule>();
			builder.RegisterModule<EventingModule>();
			builder.RegisterModule<FinanceModule>();
        }

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		[UsedImplicitly]
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

				endpointRouteBuilder.MapHub<SignalRHub>("/signalRHub");
			});
		}
    }
}
