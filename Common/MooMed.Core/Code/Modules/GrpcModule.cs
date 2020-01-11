using Autofac;
using MooMed.Core.Code.Utils;

namespace MooMed.Core.Code.Modules
{
	public class GrpcModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<SerializationModelBinderService>()
				.As<IStartable>()
				.SingleInstance();
		}
	}
}
