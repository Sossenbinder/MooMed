using Autofac;
using MooMed.IPC.EndpointResolution;
using MooMed.IPC.EndpointResolution.Interface;
using MooMed.IPC.Interface;
using MooMed.IPC.ProxyInvocation;
using MooMed.IPC.ProxyInvocation.Interface;

namespace MooMed.IPC.Module
{
	public class KubernetesModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder.RegisterType<KubernetesClientFactory>()
				.As<IKubernetesClientFactory>()
				.SingleInstance();

			builder.RegisterType<GrpcClientProvider>()
				.As<IGrpcClientProvider>()
				.SingleInstance();

			builder.RegisterType<KubernetesDiscovery>()
				.As<IKubernetesDiscovery>()
				.SingleInstance();

			builder.RegisterType<StatefulCollectionInfoProvider>()
				.As<IStatefulCollectionInfoProvider>()
				.SingleInstance();

			builder.RegisterType<DeterministicPartitionSelectorService>()
				.As<IDeterministicPartitionSelectorHelper>()
				.SingleInstance();

			builder.RegisterType<GrpcChannelProvider>()
				.As<IGrpcChannelProvider>()
				.SingleInstance();
		}
	}
}
