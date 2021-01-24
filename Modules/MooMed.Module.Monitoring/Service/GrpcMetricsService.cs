using App.Metrics;
using App.Metrics.Meter;
using App.Metrics.Timer;
using MooMed.Common.ServiceBase.ServiceBase;
using MooMed.Logging.Abstractions.Interface;
using MooMed.Module.Monitoring.Service.Interface;

namespace MooMed.Module.Monitoring.Service
{
	public class GrpcMetricsService : ServiceBaseWithLogger, IGrpcMetricsService
	{
		public MeterOptions GrpcCallMeter { get; }

		public TimerOptions GrpcCallTimer { get; }

		public GrpcMetricsService(IMooMedLogger logger)
			: base(logger)
		{
			GrpcCallMeter = new MeterOptions()
			{
				Name = "Grpc Call Meter",
				MeasurementUnit = Unit.Calls,
				RateUnit = TimeUnit.Minutes,
			};

			GrpcCallTimer = new TimerOptions()
			{
				Name = "Grpc Call Timer",
				MeasurementUnit = Unit.Requests,
				DurationUnit = TimeUnit.Microseconds,
				RateUnit = TimeUnit.Minutes,
			};
		}
	}
}