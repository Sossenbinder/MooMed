using System;
using System.Collections.Generic;
using System.Text;
using MooMed.Common.Database.Converter;
using MooMed.Common.Definitions.Models.Finance;
using MooMed.Module.Finance.Database.Entities;

namespace MooMed.Module.Finance.Converters
{
	public class ExchangeTradedEntityToModelConverter : IEntityToModelConverter<ExchangeTradedEntity, ExchangeTradedModel, string>
	{
		public ExchangeTradedModel ToModel(ExchangeTradedEntity entity)
		{
			return new ExchangeTradedModel()
			{
				XetraSymbol = entity.XetraSymbol,
				Type = entity.Type,
				TradingCurrency = entity.TradingCurrency,
				ReplicationMethod = entity.ReplicationMethod,
				ProfitUse = entity.ProfitUse,
				ProductFamily = entity.ProductFamily,
				OngoingCharges = entity.OngoingCharges,
				Isin = entity.Isin,
				FundCurrency = entity.FundCurrency,
				FeePercentage = entity.FeePercentage,
				Benchmark = entity.Benchmark,
				BloombergInav = entity.BloombergInav,
				BloombergTicker = entity.BloombergTicker,
				Homepage = entity.Homepage,
				MaxSpread = entity.MaxSpread,
				MQV = entity.MQV,
				ReutersCode = entity.ReutersCode,
				ReutersInav = entity.ReutersInav,
			};
		}
	}
}
