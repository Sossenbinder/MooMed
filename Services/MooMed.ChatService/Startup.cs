using Autofac;
using MooMed.AspNetCore.Grpc;
using MooMed.Caching.Module;
using MooMed.ChatService.Module;
using MooMed.Core;
using MooMed.Identity.Module;
using MooMed.IPC.Module;

namespace MooMed.ChatService
{
	public class Startup : GrpcEndpointStartup<Service.ChatService>
	{
		protected override void RegisterModules(ContainerBuilder containerBuilder)
		{
			base.RegisterModules(containerBuilder);

			containerBuilder.RegisterModule(new CachingModule());
			containerBuilder.RegisterModule(new KubernetesModule());
			containerBuilder.RegisterModule(new IdentityModule());
			containerBuilder.RegisterModule(new ChatServiceModule());
		}
	}
}
