using JetBrains.Annotations;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Core.Code.Logging.Loggers.Interface;

namespace MooMed.FinanceService.Service
{
	public class FinanceService : Common.ServiceBase.MooMedServiceBase, IFinanceService
	{
		public FinanceService([NotNull] IMainLogger logger) 
			: base(logger)
		{
		}
	}
}
