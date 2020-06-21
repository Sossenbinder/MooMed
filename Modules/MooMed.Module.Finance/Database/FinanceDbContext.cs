using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MooMed.Common.Database.Context;
using MooMed.Module.Finance.Database.Entities;

namespace MooMed.Module.Finance.Database
{
	public class FinanceDbContext : AbstractDbContext
	{
		[NotNull]
		public DbSet<ExchangeTradedEntity> ExchangeTradeds { get; set; }

		public FinanceDbContext(string connectionString) 
			: base(connectionString)
		{
		}
	}
}
