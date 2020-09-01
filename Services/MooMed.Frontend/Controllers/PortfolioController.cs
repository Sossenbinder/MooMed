using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MooMed.Common.Definitions.Models.Portfolio;
using MooMed.Common.Definitions.UiModels.Finance.Portfolio;
using MooMed.Frontend.Controllers.Base;
using MooMed.Frontend.Controllers.Result;
using MooMed.ServiceBase.Services.Interface;

namespace MooMed.Frontend.Controllers
{
	public class PortfolioController : SessionBaseController
	{
		[NotNull]
		private readonly IFinanceService _financeService;

		public PortfolioController(
			[NotNull] ISessionService sessionService,
			[NotNull] IFinanceService financeService)
			: base(sessionService)
		{
			_financeService = financeService;
		}

		[NotNull]
		[HttpGet]
		[ItemNotNull]
		[Authorize]
		public async Task<JsonResponse> GetPortfolio()
		{
			var response = await _financeService.GetPortfolio(CurrentSessionOrFail);

			return response.ToJsonResponse();
		}

		[NotNull]
		[HttpPost]
		[ItemNotNull]
		[Authorize]
		public async Task<JsonResponse> AddToPortfolio([FromBody] PortfolioEntryUiModel portfolioEntryUiModel)
		{
			var response = await _financeService.AddFondToPortfolio(new PortfolioItem()
			{
				Amount = portfolioEntryUiModel.Amount,
				Isin = portfolioEntryUiModel.Isin,
				SessionContext = CurrentSession,
			});

			return response.ToJsonResponse();
		}
	}
}