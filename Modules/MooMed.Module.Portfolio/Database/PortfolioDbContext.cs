using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using MooMed.Common.Database.Context;
using MooMed.Module.Portfolio.DataTypes.Entity;

namespace MooMed.Module.Portfolio.Database
{
	internal class PortfolioDbContext : AbstractDbContext
	{
		[NotNull]
		public DbSet<PortfolioMappingEntity> PortfolioMappings { get; set; }

		public PortfolioDbContext(string connectionString)
			: base(connectionString)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}
