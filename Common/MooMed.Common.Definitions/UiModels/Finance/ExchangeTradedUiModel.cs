using MooMed.Common.Definitions.Interface;
using MooMed.Common.Definitions.Models.Finance;

namespace MooMed.Common.Definitions.UiModels.Finance
{
	public class ExchangeTradedUiModel : IUiModel
	{
		public ExchangeTradedType Type { get; set; }

		public string Isin { get; set; }

		public string ProductFamily { get; set; }

		public string XetraSymbol { get; set; }

		public double? FeePercentage { get; set; }

		public double? OngoingCharges { get; set; }

		public string ProfitUse { get; set; }

		public string ReplicationMethod { get; set; }

		public string FundCurrency { get; set; }

		public string TradingCurrency { get; set; }
	}
}
