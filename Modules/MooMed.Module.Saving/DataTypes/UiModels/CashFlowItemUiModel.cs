using System;
using MooMed.Common.Definitions.Interface;
using MooMed.Common.Definitions.Models.Saving;

namespace MooMed.Module.Saving.DataTypes.UiModels
{
    public class CashFlowItemUiModel : IUiModel
    {
        public string Name { get; set; } = null!;

        public Guid Identifier { get; set; }

        public CashFlowItemType CashFlowItemType { get; set; }

        public double Amount { get; set; }

        public CashFlow FlowType { get; set; }
    }
}