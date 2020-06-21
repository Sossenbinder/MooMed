using System;
using MooMed.Common.Database.Converter;
using MooMed.Common.Definitions.Models.Finance;
using MooMed.Common.Definitions.UiModels.Finance;

namespace MooMed.Module.Finance.Converters
{
	public class GetExchangeTradedsUiModelToModelConverter : IUiModelToModelConverter<GetExchangeTradedsUiModel, GetExchangeTradedsModel>
	{
		public GetExchangeTradedsModel ToModel(GetExchangeTradedsUiModel uiModel)
		{
			return new GetExchangeTradedsModel()
			{
				ExchangeTradedType = uiModel.ExchangeTradedType,
			};
		}
	}
}
