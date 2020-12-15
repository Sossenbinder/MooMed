using JetBrains.Annotations;
using MooMed.Common.ServiceBase.ServiceBase;
using MooMed.Logging.Abstractions.Interface;
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