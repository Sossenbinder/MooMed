using Autofac;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MooMed.AspNetCore.Grpc;
using MooMed.AspNetCore.Identity.Helper;
using MooMed.Caching.Module;
using MooMed.DependencyInjection.Extensions;
using MooMed.Identity.Module;
using MooMed.IPC.Module;
using MooMed.Module.Accounts.Database;
using MooMed.Module.Accounts.Datatypes.Entity;
using MooMed.Module.Accounts.Module;
using MooMed.ServiceBase.Services.Interface;
using MooMed.AccountService.Module;
using MooMed.AccountValidationService.Remoting;
using MooMed.ProfilePictureService.Remoting;
using MooMed.SessionService.Remoting;

namespace MooMed.AccountService
{
	public class Startup : GrpcEndpointStartup<Service.AccountService>
	{
		public override void ConfigureServices(IServiceCollection services)
		{
			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(options => options.LoginPath = "/Logon/Login");

			services.AddDbContext<AccountDbContext>(options =>
				options.UseSqlServer(
					"Server=tcp:moomeddbserver.database.windows.net,1433;Initial Catalog=Main;Persist Security Info=False;User ID=moomedadmin;Password=8fC2XaAB1JPPwTL05SoFbdNRvKAH2bHy;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));

			services.AddIdentity<AccountEntity, IdentityRole<int>>(IdentityOptionsProvider.ApplyDefaultOptions)
				.AddErrorDescriber<CodeIdentityErrorDescriber>()
				.AddEntityFrameworkStores<AccountDbContext>()
				.AddDefaultTokenProviders();

			services.AddSingleton<PasswordHasher<AccountEntity>>();

			base.ConfigureServices(services);
		}

		protected override void RegisterModules(ContainerBuilder containerBuilder)
		{
			base.RegisterModules(containerBuilder);

			containerBuilder.RegisterModule<InternalAccountModule>();
			containerBuilder.RegisterModule(new AccountServiceModule());
			containerBuilder.RegisterModule(new CachingModule());
			containerBuilder.RegisterModule(new KubernetesModule());
			containerBuilder.RegisterModule(new IdentityModule());

			containerBuilder.RegisterGrpcService<IProfilePictureService, ProfilePictureServiceProxy>();
			containerBuilder.RegisterGrpcService<ISessionService, SessionServiceProxy>();
			containerBuilder.RegisterGrpcService<IAccountValidationService, AccountValidationServiceProxy>();
		}
	}
}