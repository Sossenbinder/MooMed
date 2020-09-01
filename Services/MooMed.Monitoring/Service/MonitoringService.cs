using JetBrains.Annotations;
using MooMed.Common.Definitions.Logging;
using MooMed.Common.ServiceBase.ServiceBase;
using MooMed.ServiceBase.Services.Interface;

namespace MooMed.Monitoring.Service
{
	public class MonitoringService : MooMedServiceBaseWithLogger, IMonitoringService
	{
		public MonitoringService(
			[NotNull] IMooMedLogger logger)
			: base(logger)
		{
		}
	}
}