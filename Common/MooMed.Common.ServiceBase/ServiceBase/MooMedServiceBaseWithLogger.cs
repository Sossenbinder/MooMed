using JetBrains.Annotations;
using MooMed.Logging.Abstractions.Interface;

namespace MooMed.Common.ServiceBase.ServiceBase
{
    public class MooMedServiceBaseWithLogger : MooMedServiceBase
    {
        [NotNull]
        protected readonly IMooMedLogger Logger;

        public MooMedServiceBaseWithLogger([NotNull] IMooMedLogger logger)
        {
            Logger = logger;
        }
    }
}