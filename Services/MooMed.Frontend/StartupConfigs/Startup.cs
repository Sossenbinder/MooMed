using System.Text.Json.Serialization;
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
using MooMed.Common.ServiceBase.Module;
using MooMed.Configuration.Module;
using MooMed.Core;
using MooMed.Identity.Module;
using MooMed.Encryption.Module;
using MooMed.Eventing.Helper;
using MooMed.Eventing.Module;
using MooMed.Frontend.Modules;
using MooMed.IPC.Module;
using MooMed.Logging.Module;
using MooMed.Module.Accounts.Module;
using MooMed.Module.Finance.Modules;
using MooMed.Module.Monitoring.Module;
using MooMed.Module.Saving.Modules;
using MooMed.Serialization.Module;
using MooMed.SignalR.Hubs;
using AccountValidationModule = MooMed.Module.AccountValidation.Module.AccountValidationModule;

namespace MooMed.Frontend.StartupConfigs
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureMassTransit(services);

            services.AddMvc();

            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => options.LoginPath = "/Logon/Login");

            services.AddAntiforgery(x => x.HeaderName = "AntiForgery");

            services.AddResponseCompression();
        }

        private static void ConfigureMassTransit([NotNull] IServiceCollection services)
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
            builder.RegisterModule<EncryptionModule>();
            builder.RegisterModule<ConfigurationModule>();
            builder.RegisterModule<LoggingModule>();
            builder.RegisterModule<CachingModule>();
            builder.RegisterModule<SerializationModule>();
            builder.RegisterModule<FrontendMooMedAspNetCoreModule>();
            builder.RegisterModule<KubernetesModule>();
            builder.RegisterModule<IdentityModule>();
            builder.RegisterModule<EventingModule>();
            builder.RegisterModule<FinanceModule>();
            builder.RegisterModule<ServiceBaseModule>();
            builder.RegisterModule<AccountValidationModule>();
            builder.RegisterModule<MonitoringModule>();
            builder.RegisterModule<SavingModule>();
            builder.RegisterModule<AccountModule>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [UsedImplicitly]
        public void Configure([NotNull] IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

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

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseHttpsRedirection();
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