using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using MooMed.Common.Database.Converter;
using MooMed.Common.Definitions.Models.Finance;
using MooMed.Common.Definitions.UiModels.Finance;
using MooMed.Frontend.Controllers.Base;
using MooMed.Frontend.Controllers.Result;
using MooMed.ServiceBase.Services.Interface;

namespace MooMed.Frontend.Controllers
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
		public async Task<JsonDataResponse<List<ExchangeTradedUiModel>>> GetExchangeTradeds()
		{
			var response = await _financeService.GetExchangeTradeds();

			return response.ToUiModelJsonResponse(_exchangeTradedModelToUiModelConverter);
		}
	}
}