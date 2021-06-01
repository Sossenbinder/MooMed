using Microsoft.EntityFrameworkCore;
using MooMed.Common.Database.Repository;
using MooMed.Module.Saving.Database;
using MooMed.Module.Saving.Database.Entities;
using MooMed.Module.Saving.Repository.Interface;

namespace MooMed.Module.Saving.Repository
{
	public class AssetRepository : AbstractCrudRepository<SavingDbContext, AssetsEntity, int>, IAssetRepository
	{
		public AssetRepository(IDbContextFactory<SavingDbContext> contextFactory)
			: base(contextFactory)
		{
		}
	}
}