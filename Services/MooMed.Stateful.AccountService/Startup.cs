using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using MooMed.AspNetCore.Grpc;
using MooMed.Caching.Module;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Core;
using MooMed.Core.Code.Utils;
using MooMed.IPC.Module;
using MooMed.Module.Accounts;
using MooMed.Stateful.AccountService.Module;
using MooMed.Stateful.ProfilePictureService.Remoting;
using MooMed.Stateful.SessionService.Remoting;

namespace MooMed.Stateful.AccountService
{
	public class Startup : GrpcEndpointStartup
	{
		protected override void RegisterServices(IEndpointRouteBuilder endpointRouteBuilder)
		{
			endpointRouteBuilder.MapGrpcService<Service.AccountService>();
		}

		protected override void RegisterModules(ContainerBuilder containerBuilder)
		{
			containerBuilder.RegisterModule(new AccountServiceBindings());
			containerBuilder.RegisterModule(new CoreBindings());
			containerBuilder.RegisterModule(new AccountServiceModule());
			containerBuilder.RegisterModule(new CachingModule());
			containerBuilder.RegisterModule(new KubernetesModule());

			containerBuilder.RegisterType<ProfilePictureServiceProxy>()
				.As<IProfilePictureService>()
				.SingleInstance();

			containerBuilder.RegisterType<SessionServiceProxy>()
				.As<ISessionService>()
				.SingleInstance();
		}
	}
}
