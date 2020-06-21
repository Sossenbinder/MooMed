using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.Finance;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Core.Code.Logging.Loggers.Interface;
using MooMed.Core.DataTypes;
using MooMed.Module.Finance.Service.Interface;

namespace MooMed.FinanceService.Service
{
	public class FinanceService : Common.ServiceBase.MooMedServiceBase, IFinanceService
	{
		[NotNull]
		private readonly IExchangeTradedsService _exchangeTradedsService;

		public FinanceService(
			[NotNull] IMainLogger logger,
			[NotNull] IExchangeTradedsService exchangeTradedsService)
			: base(logger)
		{
			_exchangeTradedsService = exchangeTradedsService;
		}

		public Task<ServiceResponse<IEnumerable<ExchangeTradedModel>>> GetExchangeTradeds()
		{
			return _exchangeTradedsService.GetExchangeTradeds();
		}
	}
}
