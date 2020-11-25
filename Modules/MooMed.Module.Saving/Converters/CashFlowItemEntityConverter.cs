using MooMed.Common.Database.Converter;
using MooMed.Common.Definitions.Models.Saving;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Module.Saving.Database.Entities;

namespace MooMed.Module.Saving.Converters
{
    public class CashFlowItemEntityConverter : IBiDirectionalDbConverter<CashFlowItem, CashFlowItemEntity, int>
    {
        public CashFlowItemEntity ToEntity(CashFlowItem model, ISessionContext sessionContext = null!)
        {
            return new CashFlowItemEntity()
            {
                Amount = model.Amount,
                CashFlowItemType = model.CashFlowItemType,
                FlowType = model.FlowType,
                Id = sessionContext!.Account.Id,
                Name = model.Name,
                Identifier = model.Identifier,
            };
        }

        public CashFlowItem ToModel(CashFlowItemEntity entity)
        {
            return new CashFlowItem()
            {
                Identifier = entity.Identifier,
                Name = entity.Name,
                Amount = entity.Amount,
                CashFlowItemType = entity.CashFlowItemType,
                FlowType = entity.FlowType,
            };
        }
    }
}