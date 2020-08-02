using Autofac;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MooMed.AspNetCore.Grpc;
using MooMed.AspNetCore.Identity.Helper;
using MooMed.Caching.Module;
using MooMed.Identity.Module;
using MooMed.IPC.Module;
using MooMed.Module.Accounts.Database;
using MooMed.Module.Accounts.Datatypes.Entity;
using MooMed.Module.Accounts.Helper;
using MooMed.Module.Accounts.Module;
using MooMed.Stateful.AccountValidationService.Module;

namespace MooMed.Stateful.AccountValidationService
{
	public class Startup : GrpcEndpointStartup<Service.AccountValidationService>
	{
		public override void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<AccountDbContext>(options =>
				options.UseSqlServer(
					"Server=tcp:moomeddbserver.database.windows.net,1433;Initial Catalog=Main;Persist Security Info=False;User ID=moomedadmin;Password=8fC2XaAB1JPPwTL05SoFbdNRvKAH2bHy;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));

			services.AddIdentity<AccountEntity, IdentityRole<int>>(IdentityOptionsProvider.ApplyDefaultOptions)
				.AddUserValidator<CustomDuplicateEmailUserValidator<AccountEntity>>()
				.AddErrorDescriber<CodeIdentityErrorDescriber>()
				.AddEntityFrameworkStores<AccountDbContext>()
				.AddDefaultTokenProviders();

			base.ConfigureServices(services);
		}

		protected override void RegisterModules(ContainerBuilder containerBuilder)
		{
			base.RegisterModules(containerBuilder);

			containerBuilder.RegisterModule<AccountValidationModule>();
			containerBuilder.RegisterModule<AccountValidationServiceModule>();
			containerBuilder.RegisterModule<CachingModule>();
			containerBuilder.RegisterModule<KubernetesModule>();
			containerBuilder.RegisterModule<IdentityModule>();

			containerBuilder.RegisterModule<MooMed.Module.AccountValidation.Module.InternalAccountValidationModule>();
		}
	}
}