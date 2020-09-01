using Autofac;
using MooMed.Module.Monitoring.Service;
using MooMed.Module.Monitoring.Service.Interface;

namespace MooMed.Module.Monitoring.Module
{
	public class MonitoringModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterModule<InternalMonitoringModule>();

			builder.RegisterType<GrpcMetricsService>()
				.As<IGrpcMetricsService>()
				.SingleInstance()
				.AutoActivate();
		}
	}
}