using System;
using Autofac;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MooMed.Caching.Module;
using MooMed.Core;
using MooMed.Dns.Module;
using MooMed.Eventing.Module;
using MooMed.IPC.Module;
using MooMed.Web.Hubs;
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
		        services.AddMvc();
		        services.AddSignalR();
				services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => options.LoginPath = "/Logon/Login");
			}
	        catch (DllNotFoundException)
	        {
				// TODO: Figure out why this is being raised
	        }
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
