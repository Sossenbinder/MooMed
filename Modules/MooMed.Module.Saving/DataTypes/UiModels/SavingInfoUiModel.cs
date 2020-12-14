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

    public class SavingInfoUiModel : IUiModel
    {
        public int? Currency { get; set; }

        public BasicSavingInfoUiModel? BasicSavingInfo { get; set; }

        public List<CashFlowItemUiModel>? FreeFormSavingModels { get; set; }
    }
}