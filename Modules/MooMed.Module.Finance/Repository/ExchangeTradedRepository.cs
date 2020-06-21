using JetBrains.Annotations;
using MooMed.Common.Database.Context;
using MooMed.Common.Database.Repository;
using MooMed.Module.Finance.Database;
using MooMed.Module.Finance.Database.Entities;
using MooMed.Module.Finance.Repository.Interface;

namespace MooMed.Module.Finance.Repository
{
	public class ExchangeTradedRepository : AbstractCrudRepository<FinanceDbContext, ExchangeTradedEntity, string>, IExchangeTradedRepository
	{
		public ExchangeTradedRepository([NotNull] AbstractDbContextFactory<FinanceDbContext> contextFactory) 
			: base(contextFactory)
		{
		}
	}
}
