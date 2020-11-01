using Autofac;
using MooMed.Serialization.Service;
using MooMed.Serialization.Service.Interface;

namespace MooMed.Serialization.Module
{
	public class SerializationModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<JsonSerializationService>()
				.As<ISerializationService>()
				.SingleInstance();
		}
	}
}