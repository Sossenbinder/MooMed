using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MooMed.Common.Database.Context;
using MooMed.Module.Saving.Database.Entities;

namespace MooMed.Module.Saving.Database
{
	public class SavingDbContext : AbstractDbContext
	{
		[NotNull]
		public DbSet<CurrencyMappingEntity> CurrencyMapping { get; set; } = null!;

		public SavingDbContext(string connectionString)
			: base(connectionString)
		{
		}
	}
}