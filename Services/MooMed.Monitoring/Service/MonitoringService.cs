using JetBrains.Annotations;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Logging.Loggers.Interface;

namespace MooMed.Monitoring.Service
{
	public class MonitoringService : Common.ServiceBase.MooMedServiceBase, IMonitoringService
	{
		public MonitoringService([NotNull] IMooMedLogger logger) 
			: base(logger)
		{
		}
	}
}
