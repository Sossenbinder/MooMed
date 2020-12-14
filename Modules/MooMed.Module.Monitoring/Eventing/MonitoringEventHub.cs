using MooMed.Common.Definitions.Eventing.Monitoring;
using MooMed.Common.Definitions.Logging;
using MooMed.Eventing.Events;
using MooMed.Eventing.Events.MassTransit.Interface;
using MooMed.Module.Monitoring.Eventing.Interface;

namespace MooMed.Module.Monitoring.Eventing
{
    public class MonitoringEventHub : IMonitoringEventHub
    {
        public MtMooEvent<GrpcCall> GrpcCallPerformed { get; }

        public MonitoringEventHub(
            IMassTransitEventingService eventingService,
            IMooMedLogger logger)
        {
            GrpcCallPerformed = new MtMooEvent<GrpcCall>(nameof(GrpcCallPerformed), eventingService, logger);
        }
    }
}