using System.Linq;
using MooMed.Common.Database.Converter;
using MooMed.Common.Definitions.Models.Saving;
using MooMed.Module.Saving.DataTypes.UiModels;

namespace MooMed.Module.Saving.Converters
{
    public class SavingModelToUiModelConverter : IModelToUiModelConverter<SavingInfoModel, SavingInfoUiModel>
    {
        public SavingInfoUiModel ToUiModel(SavingInfoModel model)
        {
            var savingUiModel = new SavingInfoUiModel
            {
                Currency = model.Currency != null ? (int)model.Currency : (int?)null,
                FreeFormSavingModels = model.FreeFormSavingInfo?.Select(CashFlowItemAsUiModel).ToList()
            };

            if (model.BasicSavingInfo != null)
            {
                savingUiModel.BasicSavingInfo = new BasicSavingInfoUiModel()
                {
                    Income = CashFlowItemAsUiModel(model.BasicSavingInfo.Income),
                    Rent = CashFlowItemAsUiModel(model.BasicSavingInfo.Rent),
                    Groceries = CashFlowItemAsUiModel(model.BasicSavingInfo.Groceries),
                };
            }

            return savingUiModel;
        }

        private static CashFlowItemUiModel CashFlowItemAsUiModel(CashFlowItem cashflowItem)
        {
            return new CashFlowItemUiModel()
            {
                Name = cashflowItem.Name,
                Amount = cashflowItem.Amount,
                CashFlowItemType = cashflowItem.CashFlowItemType,
                FlowType = cashflowItem.FlowType,
                Identifier = cashflowItem.Identifier
            };
        }
    }
}