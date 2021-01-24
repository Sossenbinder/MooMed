using MooMed.Common.Definitions.Eventing.Monitoring;
using MooMed.Eventing.Events;

namespace MooMed.Module.Monitoring.Eventing.Interface
{
	public interface IMonitoringEventHub
	{
		MtEvent<GrpcCall> GrpcCallPerformed { get; }
	}
}