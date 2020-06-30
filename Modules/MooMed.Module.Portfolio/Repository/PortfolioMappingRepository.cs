using JetBrains.Annotations;
using MooMed.Common.Database.Context;
using MooMed.Common.Database.Repository;
using MooMed.Module.Portfolio.Database;
using MooMed.Module.Portfolio.DataTypes.Entity;
using MooMed.Module.Portfolio.Repository.Interface;

namespace MooMed.Module.Portfolio.Repository
{
	internal class PortfolioMappingRepository : AbstractCrudRepository<PortfolioDbContext, PortfolioMappingEntity, int>, IPortfolioMappingRepository
	{
		public PortfolioMappingRepository([NotNull] AbstractDbContextFactory<PortfolioDbContext> contextFactory) 
			: base(contextFactory)
		{
		}
	}
}
