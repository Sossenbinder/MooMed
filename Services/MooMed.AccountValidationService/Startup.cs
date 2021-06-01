using Autofac;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MooMed.AspNetCore.Grpc;
using MooMed.AspNetCore.Identity.Helper;
using MooMed.Caching.Module;
using MooMed.Configuration.Module;
using MooMed.Identity.Module;
using MooMed.IPC.Module;
using MooMed.Module.Accounts.Database;
using MooMed.Module.Accounts.Datatypes.Entity;
using MooMed.Module.Accounts.Module;
using MooMed.AccountValidationService.Module;

namespace MooMed.AccountValidationService
{
	public class Startup : GrpcEndpointStartup<Service.AccountValidationService>
	{
		private readonly IConfiguration _configuration;

		public Startup(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public override void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<AccountDbContext>(options => options.UseSqlServer(_configuration["IdentityConnectionString"]));

			services.AddIdentity<AccountEntity, IdentityRole<int>>(IdentityOptionsProvider.ApplyDefaultOptions)
				.AddErrorDescriber<CodeIdentityErrorDescriber>()
				.AddEntityFrameworkStores<AccountDbContext>()
				.AddDefaultTokenProviders();

			base.ConfigureServices(services);
		}

		protected override void RegisterModules(ContainerBuilder containerBuilder)
		{
			base.RegisterModules(containerBuilder);

			containerBuilder.RegisterModule<ConfigurationModule>();
			containerBuilder.RegisterModule<AccountValidationModule>();
			containerBuilder.RegisterModule<AccountValidationServiceModule>();
			containerBuilder.RegisterModule<CachingModule>();
			containerBuilder.RegisterModule<KubernetesModule>();
			containerBuilder.RegisterModule<IdentityModule>();

			containerBuilder.RegisterModule<MooMed.Module.AccountValidation.Module.InternalAccountValidationModule>();
		}
	}
}