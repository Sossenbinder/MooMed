using System.Collections.Generic;
using MooMed.Common.Definitions.Interface;
using MooMed.Common.Definitions.Models.Saving;

namespace MooMed.Module.Saving.DataTypes.UiModels
{
    public class BasicSavingInfoUiModel : IUiModel
    {
        public CashFlowItemUiModel Income { get; set; }

        public CashFlowItemUiModel Rent { get; set; }

        public CashFlowItemUiModel Groceries { get; set; }
    }

    public class SavingInfoUiModel : IUiModel
    {
        public int? Currency { get; set; }

        public BasicSavingInfoUiModel? BasicSavingInfo { get; set; }

        public List<CashFlowItemUiModel>? FreeFormSavingModels { get; set; }
    }
}