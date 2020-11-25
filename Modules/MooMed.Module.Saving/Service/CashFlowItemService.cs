using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MooMed.Common.Definitions.Models.Saving;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Module.Saving.Converters;
using MooMed.Module.Saving.Repository.Interface;
using MooMed.Module.Saving.Service.Interface;

namespace MooMed.Module.Saving.Service
{
    public class CashFlowItemService : ICashFlowItemService
    {
        private readonly ICashFlowItemRepository _cashFlowItemRepository;

        private readonly CashFlowItemEntityConverter _cashFlowItemEntityConverter;

        public CashFlowItemService(
            ICashFlowItemRepository cashFlowItemRepository,
            CashFlowItemEntityConverter cashFlowItemEntityConverter)
        {
            _cashFlowItemRepository = cashFlowItemRepository;
            _cashFlowItemEntityConverter = cashFlowItemEntityConverter;
        }

        public async Task SaveCashFlowItems(ISessionContext sessionContext, List<CashFlowItem> cashFlowItems)
        {
            var entities = cashFlowItems.ConvertAll(x => _cashFlowItemEntityConverter.ToEntity(x, sessionContext));

            await _cashFlowItemRepository.CreateOrUpdate(entities, x =>
            {
                var item = entities.FirstOrDefault(y => x == y)!;

                x.Amount = item.Amount;
                x.Name = item.Name;
            });
        }

        public async Task<IEnumerable<CashFlowItem>> GetCashFlowItems(ISessionContext sessionContext)
        {
            var cashFlowItems = await _cashFlowItemRepository.Read(x => x.Id == sessionContext.Account.Id);

            return cashFlowItems.Select(_cashFlowItemEntityConverter.ToModel);
        }
    }
}