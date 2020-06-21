using MooMed.Common.Definitions.Interface;
using MooMed.Common.Definitions.Models.Finance;

namespace MooMed.Common.Definitions.UiModels.Finance
{
	public class GetExchangeTradedsUiModel : IUiModel
	{
		public ExchangeTradedType? ExchangeTradedType { get; set; }
	}
}
