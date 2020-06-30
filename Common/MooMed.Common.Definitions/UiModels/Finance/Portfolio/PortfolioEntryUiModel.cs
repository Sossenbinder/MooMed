using System;
using System.Collections.Generic;
using System.Text;
using MooMed.Common.Definitions.Interface;

namespace MooMed.Common.Definitions.UiModels.Finance.Portfolio
{
	public class PortfolioEntryUiModel : IUiModel
	{
		public string Isin { get; set; }

		public int Amount { get; set; }
	}
}
