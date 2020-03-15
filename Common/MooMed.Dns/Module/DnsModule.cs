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
			
			builder
#if DEBUG
				.RegisterType<DockerDnsResolutionService>()
#else
				.RegisterType<KubernetesDnsResolutionService>()
#endif
				.As<IDnsResolutionService>()
				.SingleInstance();
		}
	}
}
