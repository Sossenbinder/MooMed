using Autofac;
using MooMed.IPC.EndpointResolution;
using MooMed.IPC.EndpointResolution.Interface;
using MooMed.IPC.Grpc;
using MooMed.IPC.Grpc.Interface;
using MooMed.IPC.Interface;

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

			builder.RegisterType<EndpointProvider>()
				.As<IEndpointProvider>()
				.SingleInstance();

			builder.RegisterType<DeterministicPartitionSelectorService>()
				.As<IDeterministicPartitionSelectorHelper>()
				.SingleInstance();

			// Client / Channel providers
			builder.RegisterType<GrpcClientProvider>()
				.As<IGrpcClientProvider>()
				.SingleInstance();

			builder.RegisterType<GrpcChannelProvider>()
				.As<IGrpcChannelProvider>()
				.SingleInstance();

			builder.RegisterType<SpecificGrpcClientProvider>()
				.As<ISpecificGrpcClientProvider>()
				.SingleInstance();

			builder.RegisterType<SpecificGrpcChannelProvider>()
				.As<ISpecificGrpcChannelProvider>()
				.SingleInstance();
		}
	}
}