using Autofac;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MooMed.AspNetCore.Grpc;
using MooMed.Caching.Module;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.ServiceBase.Interface;
using MooMed.DependencyInjection.Extensions;
using MooMed.Dns.Module;
using MooMed.IPC.Module;
using MooMed.Module.Accounts.Database;
using MooMed.Module.Accounts.Module;
using MooMed.Stateful.AccountService.Module;
using MooMed.Stateful.AccountValidationService.Remoting;
using MooMed.Stateful.ProfilePictureService.Remoting;
using MooMed.Stateful.SessionService.Remoting;

namespace MooMed.Stateful.AccountService
{
	public class Startup : GrpcEndpointStartup<Service.AccountService>
	{
		public override void ConfigureServices(IServiceCollection services)
		{
			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(options => options.LoginPath = "/Logon/Login");

			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(
					"Server=tcp:moomeddbserver.database.windows.net,1433;Initial Catalog=Main;Persist Security Info=False;User ID=moomedadmin;Password=8fC2XaAB1JPPwTL05SoFbdNRvKAH2bHy;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));

			services.AddIdentity<Account, IdentityRole<int>>(options =>
			{
				options.SignIn.RequireConfirmedEmail = true;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = false;
			}).AddEntityFrameworkStores<ApplicationDbContext>();

			services.AddSingleton<PasswordHasher<Account>>();

			base.ConfigureServices(services);
		}

		protected override void RegisterModules(ContainerBuilder containerBuilder)
		{
			base.RegisterModules(containerBuilder);

			containerBuilder.RegisterModule<InternalAccountModule>();
			containerBuilder.RegisterModule(new AccountServiceModule());
			containerBuilder.RegisterModule(new CachingModule());
			containerBuilder.RegisterModule(new KubernetesModule());
			containerBuilder.RegisterModule(new DnsModule());

			containerBuilder.RegisterGrpcService<IProfilePictureService, ProfilePictureServiceProxy>();
			containerBuilder.RegisterGrpcService<ISessionService, SessionServiceProxy>();
			containerBuilder.RegisterGrpcService<IAccountValidationService, AccountValidationServiceProxy>();
		}
	}
}