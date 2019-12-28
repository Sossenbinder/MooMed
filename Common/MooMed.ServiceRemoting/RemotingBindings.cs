using Autofac;
using JetBrains.Annotations;
using MooMed.ServiceRemoting.EndpointResolution;
using MooMed.ServiceRemoting.EndpointResolution.Interface;
using MooMed.ServiceRemoting.Interface;
using MooMed.ServiceRemoting.ProxyInvocation;
using MooMed.ServiceRemoting.ProxyInvocation.Interface;

namespace MooMed.ServiceRemoting
{
	public class RemotingBindings : Autofac.Module
	{
		protected override void Load([NotNull] ContainerBuilder builder)
		{
			builder.RegisterType<RemotingProxyProvider>()
				.As<IRemotingProxyProvider>()
				.SingleInstance();

			builder.RegisterType<ServiceFabricServiceResolver>()
				.As<IServiceFabricServiceResolver>()
				.SingleInstance();

			builder.RegisterType<ServiceFabricEndpointManager>()
				.As<IServiceFabricEndpointManager>()
				.SingleInstance();

			builder.RegisterType<PartitionInfoProvider>()
				.As<IPartitionInfoProvider>()
				.SingleInstance();

			builder.RegisterType<DeterministicPartitionSelectorService>()
				.As<IDeterministicPartitionSelectorHelper>()
				.SingleInstance();
		}
	}
}
