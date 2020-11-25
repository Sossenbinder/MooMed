using System.Collections.Generic;
using MooMed.Common.Definitions.Interface;

namespace MooMed.Module.Saving.DataTypes.UiModels
{
    public class SetCashFlowItemsUiModel : IUiModel
    {
        public List<CashFlowItemUiModel> CashFlowItems { get; set; }
    }
}