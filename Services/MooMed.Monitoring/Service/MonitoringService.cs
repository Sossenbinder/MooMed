using JetBrains.Annotations;
using MooMed.Common.ServiceBase.ServiceBase;
using MooMed.Grpc.Services.Interface;
using MooMed.Logging.Loggers.Interface;

namespace MooMed.Monitoring.Service
{
	public class MonitoringService : MooMedServiceBaseWithLogger, IMonitoringService
	{
		public MonitoringService([NotNull] IMooMedLogger logger) 
			: base(logger)
		{
		}
	}
}
