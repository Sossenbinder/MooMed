using JetBrains.Annotations;
using MooMed.Common.Database.Context.Interface;
using MooMed.Common.Database.Repository;
using MooMed.Module.Saving.Database;
using MooMed.Module.Saving.Database.Entities;
using MooMed.Module.Saving.Repository.Interface;

namespace MooMed.Module.Saving.Repository
{
	public class CurrencyMappingRepository : AbstractCrudRepository<SavingDbContext, CurrencyMappingEntity, int>, ICurrencyMappingRepository
	{
		public CurrencyMappingRepository([NotNull] IDbContextFactory<SavingDbContext> contextFactory) : base(contextFactory)
		{ }
	}
}