using Autofac;
using MooMed.Common.Definitions.Eventing.Monitoring;
using MooMed.Module.Monitoring.Eventing;
using MooMed.Module.Monitoring.Eventing.Interface;

namespace MooMed.Module.Monitoring.Module
{
	public class InternalMonitoringModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<MonitoringEventHub>()
				.As<IMonitoringEventHub>()
				.SingleInstance();
		}
	}
}