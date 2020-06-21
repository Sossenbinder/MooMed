using JetBrains.Annotations;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Core.Code.Logging.Loggers.Interface;

namespace MooMed.Monitoring.Service
{
	public class MonitoringService : Common.ServiceBase.MooMedServiceBase, IMonitoringService
	{
		public MonitoringService([NotNull] IMainLogger logger) 
			: base(logger)
		{
		}
	}
}
