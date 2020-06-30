using System;
using System.Collections.Generic;
using System.Text;
using MooMed.Common.Database.Converter;
using MooMed.Common.Definitions.Models.Finance;
using MooMed.Common.Definitions.UiModels.Finance;

namespace MooMed.Module.Finance.Converters
{
	public class ExchangeTradedModelToUiModelConverter : IModelToUiModelConverter<ExchangeTraded, ExchangeTradedUiModel>
	{
		public ExchangeTradedUiModel ToUiModel(ExchangeTraded model)
		{
			return new ExchangeTradedUiModel()
			{
				FeePercentage = model.FeePercentage,
				FundCurrency = model.FundCurrency,
				Isin = model.Isin,
				OngoingCharges = model.OngoingCharges,
				ProductFamily = model.ProductFamily,
				ProfitUse = model.ProfitUse,
				ReplicationMethod = model.ReplicationMethod,
				TradingCurrency = model.TradingCurrency,
				Type = model.Type,
				XetraSymbol = model.XetraSymbol,
			};
		}
	}
}
