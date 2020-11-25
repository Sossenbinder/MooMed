using MooMed.Common.Database.Repository.Interface;
using MooMed.Module.Saving.Database.Entities;

namespace MooMed.Module.Saving.Repository.Interface
{
    public interface ICashFlowItemRepository : ICrudRepository<CashFlowItemEntity, int>
    {
    }
}