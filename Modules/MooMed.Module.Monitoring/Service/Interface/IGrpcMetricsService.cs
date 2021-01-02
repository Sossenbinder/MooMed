using App.Metrics.Meter;
using App.Metrics.Timer;

namespace MooMed.Module.Monitoring.Service.Interface
{
    public interface IGrpcMetricsService
    {
        public MeterOptions GrpcCallMeter { get; }

        public TimerOptions GrpcCallTimer { get; }
    }
}