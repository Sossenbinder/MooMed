using Autofac;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
using MooMed.RemotingProxies.Proxies;
using MooMed.Serialization.Module;

namespace MooMed.AccountService
{
	public class Startup : GrpcEndpointStartup<Service.AccountService>
	{
		private readonly IConfiguration _configuration;

		public Startup(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public override void ConfigureServices(IServiceCollection services)
		{
			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(options => options.LoginPath = "/Logon/Login");

			services.AddDbContext<AccountDbContext>(options => options.UseSqlServer(_configuration["IdentityConnectionString"]));

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
			containerBuilder.RegisterModule<SerializationModule>();
			containerBuilder.RegisterModule(new KubernetesModule());
			containerBuilder.RegisterModule(new IdentityModule());

			containerBuilder.RegisterGrpcService<IProfilePictureService, ProfilePictureServiceProxy>();
			containerBuilder.RegisterGrpcService<ISessionService, SessionServiceProxy>();
			containerBuilder.RegisterGrpcService<IAccountValidationService, AccountValidationServiceProxy>();
		}
	}
}