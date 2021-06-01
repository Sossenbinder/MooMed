using System.Collections.Generic;
using MooMed.Common.Definitions.Interface;

namespace MooMed.Module.Saving.DataTypes.UiModels
{
	public class BasicSavingInfoUiModel : IUiModel
	{
		public CashFlowItemUiModel Income { get; set; } = null!;

		public CashFlowItemUiModel Rent { get; set; } = null!;

		public CashFlowItemUiModel Groceries { get; set; } = null!;
	}

	public class AssetsUiModel : IUiModel
	{
		public int Cash { get; set; }

		public int Debt { get; set; }

		public int Equity { get; set; }

		public int Estate { get; set; }

		public int Commodities { get; set; }
	}

	public class SavingInfoUiModel : IUiModel
	{
		public int? Currency { get; set; }

		public BasicSavingInfoUiModel? BasicSavingInfo { get; set; }

		public AssetsUiModel? Assets { get; set; }

		public List<CashFlowItemUiModel>? FreeFormSavingModels { get; set; }
	}
}