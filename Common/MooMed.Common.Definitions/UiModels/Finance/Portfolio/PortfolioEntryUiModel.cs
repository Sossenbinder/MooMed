using MooMed.Common.Definitions.Interface;

namespace MooMed.Common.Definitions.UiModels.Finance.Portfolio
{
	public class PortfolioEntryUiModel : IUiModel
	{
		public string Isin { get; set; }

		public float Amount { get; set; }
	}
}