using Microsoft.EntityFrameworkCore;
using MooMed.Common.Database.Repository;
using MooMed.Module.Saving.Database;
using MooMed.Module.Saving.Database.Entities;
using MooMed.Module.Saving.Repository.Interface;

namespace MooMed.Module.Saving.Repository
{
    public class CashFlowItemTypeRepository : AbstractCrudRepository<SavingDbContext, CashFlowItemEntity, int>, ICashFlowItemRepository
    {
        public CashFlowItemTypeRepository(IDbContextFactory<SavingDbContext> contextFactory)
            : base(contextFactory)
        {
        }
    }
}