using Microsoft.EntityFrameworkCore;
using MooMed.Common.Database.Context;
using MooMed.Module.Saving.Database.Entities;

namespace MooMed.Module.Saving.Database
{
	public class SavingDbContext : AbstractDbContext
	{
		public DbSet<AssetsEntity> Assets { get; set; } = null!;

		public DbSet<CurrencyMappingEntity> CurrencyMapping { get; set; } = null!;

		public DbSet<CashFlowItemEntity> CashFlowItems { get; set; } = null!;

		public SavingDbContext(string connectionString)
			: base(connectionString)
		{
		}
	}
}