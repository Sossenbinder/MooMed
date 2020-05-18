using Autofac;
using MooMed.Dns.Service;
using MooMed.Dns.Service.Interface;

namespace MooMed.Dns.Module
{
	public class DnsModule : Autofac.Module
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
		}
	}
}
