using MooMed.Common.Definitions.Interface;
using MooMed.Common.Definitions.Models.Finance;

namespace MooMed.Common.Definitions.UiModels.Finance
{
	public class ExchangeTradedUiModel : IUiModel
	{
		public ExchangeTradedType Type { get; set; }

		public string Isin { get; set; } = null!;

		public string ProductFamily { get; set; } = null!;

		public string XetraSymbol { get; set; } = null!;

		public double? FeePercentage { get; set; }

		public double? OngoingCharges { get; set; }

		public string ProfitUse { get; set; } = null!;

		public string ReplicationMethod { get; set; } = null!;

		public string FundCurrency { get; set; } = null!;

		public string TradingCurrency { get; set; } = null!;
	}
}