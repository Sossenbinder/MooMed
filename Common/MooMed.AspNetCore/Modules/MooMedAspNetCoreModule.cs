using Autofac;
using MooMed.AspNetCore.Grpc.Serialization;

namespace MooMed.AspNetCore.Modules
{
	public class MooMedAspNetCoreModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<GrpcServiceTypeSerializer>()
				.AsSelf()
				.SingleInstance();

			builder.RegisterType<SerializationModelBinderService>()
				.As<IStartable>()
				.SingleInstance();

			builder.RegisterType<SerializationHelper>()
				.AsSelf()
				.SingleInstance();
		}
	}
}