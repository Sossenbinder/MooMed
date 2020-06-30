using System;
using MooMed.Common.Database.Converter;
using MooMed.Common.Definitions.Models.Finance;
using MooMed.Common.Definitions.UiModels.Finance;

namespace MooMed.Module.Finance.Converters
{
	public class GetExchangeTradedsUiModelToModelConverter : IUiModelToModelConverter<GetExchangeTradedsUiModel, GetExchangeTradeds>
	{
		public GetExchangeTradeds ToModel(GetExchangeTradedsUiModel uiModel)
		{
			return new GetExchangeTradeds()
			{
				ExchangeTradedType = uiModel.ExchangeTradedType,
			};
		}
	}
}
