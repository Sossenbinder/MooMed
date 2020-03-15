using Autofac;
using MooMed.Eventing.Events.MassTransit;
using MooMed.Eventing.Events.MassTransit.Interface;

namespace MooMed.Eventing.Module
{
	public class EventingModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder.RegisterType<MassTransitEventingService>()
				.As<IStartable, IMassTransitEventingService>()
				.SingleInstance();
		}
	}
}
