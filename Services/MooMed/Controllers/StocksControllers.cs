using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MooMed.Common.Database.Converter;
using MooMed.Common.Definitions.Models.Finance;
using MooMed.Common.Definitions.UiModels.Finance;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Web.Controllers.Base;
using MooMed.Web.Controllers.Result;
using JetBrains.Annotations;

namespace MooMed.Web.Controllers
{
	public class StocksController : BaseController
	{
		[NotNull]
		private readonly IFinanceService _financeService;

		[NotNull]
		private readonly IModelToUiModelConverter<ExchangeTraded, ExchangeTradedUiModel> _exchangeTradedModelToUiModelConverter;

		public StocksController(
			[NotNull] IFinanceService financeService,
			[NotNull] IModelToUiModelConverter<ExchangeTraded, ExchangeTradedUiModel> exchangeTradedModelToUiModelConverter)
		{
			_financeService = financeService;
			_exchangeTradedModelToUiModelConverter = exchangeTradedModelToUiModelConverter;
		}

		[NotNull]
		[HttpGet]
		public async Task<JsonResponse> GetExchangeTradeds()
		{
			var response = await _financeService.GetExchangeTradeds();

			return response.ToUiModelJsonResponse(_exchangeTradedModelToUiModelConverter);
		}
	}
}
