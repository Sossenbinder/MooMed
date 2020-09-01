using System.Diagnostics.CodeAnalysis;
using MooMed.Common.Definitions.Eventing.Monitoring;
using MooMed.Eventing.Events;
using MooMed.Eventing.Events.MassTransit.Interface;
using MooMed.Module.Monitoring.Eventing.Interface;

namespace MooMed.Module.Monitoring.Eventing
{
	public class MonitoringEventHub : IMonitoringEventHub
	{
		public MtMooEvent<GrpcCall> GrpcCallPerformed { get; }

		public MonitoringEventHub([NotNull] IMassTransitEventingService eventingService)
		{
			GrpcCallPerformed = new MtMooEvent<GrpcCall>(nameof(GrpcCallPerformed), eventingService);
		}
	}
}