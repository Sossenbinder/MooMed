using Autofac;
using MooMed.Monitoring.Remoting;
using MooMed.ServiceBase.Services.Interface;

namespace MooMed.Monitoring.Module
{
	public class MonitoringModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<MonitoringServiceProxy>()
				.As<IMonitoringService>()
				.SingleInstance();
		}
	}
}