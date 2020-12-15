using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.Finance;
using MooMed.Common.Definitions.Models.Portfolio;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.ServiceBase.ServiceBase;
using MooMed.Core.DataTypes;
using MooMed.Logging.Abstractions.Interface;
using MooMed.Module.Finance.Service.Interface;
using MooMed.Module.Portfolio.Service.Interface;
using MooMed.ServiceBase.Services.Interface;

namespace MooMed.FinanceService.Service
{
    public class FinanceService : MooMedServiceBaseWithLogger, IFinanceService
    {
        [NotNull]
        private readonly IExchangeTradedsService _exchangeTradedsService;

        [NotNull]
        private readonly IPortfolioService _portfolioService;

        public FinanceService(
            [NotNull] IMooMedLogger logger,
            [NotNull] IExchangeTradedsService exchangeTradedsService,
            [NotNull] IPortfolioService portfolioService)
            : base(logger)
        {
            _exchangeTradedsService = exchangeTradedsService;
            _portfolioService = portfolioService;
        }

        public Task<ServiceResponse<IEnumerable<ExchangeTraded>>> GetExchangeTradeds()
        {
            return _exchangeTradedsService.GetExchangeTradeds();
        }

        public async Task<ServiceResponse<Portfolio>> GetPortfolio(ISessionContext sessionContext)
        {
            var portfolio = await _portfolioService.GetPortfolio(sessionContext);

            return ServiceResponse<Portfolio>.Success(portfolio);
        }

        public async Task<ServiceResponse> AddFondToPortfolio(PortfolioItem portfolioItem)
        {
            var success = await _portfolioService.AddFondToPortfolio(portfolioItem);

            return new ServiceResponse(success);
        }
    }
}