using System.Collections.Generic;
using System.Threading.Tasks;
using MooMed.Common.Definitions.Models.Saving;
using MooMed.Common.Definitions.Models.Session.Interface;

namespace MooMed.Module.Saving.Service.Interface
{
    public interface ICashFlowItemService
    {
        Task SaveCashFlowItems(ISessionContext sessionContext, List<CashFlowItem> cashFlowItems);

        Task<IEnumerable<CashFlowItem>> GetCashFlowItems(ISessionContext sessionContext);
    }
}