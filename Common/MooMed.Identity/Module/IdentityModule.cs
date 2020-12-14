using Autofac;
using MooMed.Identity.Service;
using MooMed.Identity.Service.Identity;
using MooMed.Identity.Service.Identity.Interface;
using MooMed.Identity.Service.Interface;

namespace MooMed.Identity.Module
{
    public class IdentityModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<DnsResolutionService>()
                .As<IDnsResolutionService>()
                .SingleInstance();

            builder
#if DEBUG
				.RegisterType<DockerComposeEndpointDiscoveryService>()
#else
                .RegisterType<KubernetesEndpointDnsDiscoveryService>()
#endif
                .As<IEndpointDiscoveryService>()
                .SingleInstance();

            builder.RegisterType<ServiceIdentityProvider>()
                .As<IServiceIdentityProvider>()
                .SingleInstance();
        }
    }
}