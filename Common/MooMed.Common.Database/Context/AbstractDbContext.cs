using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace MooMed.Common.Database.Context
{
	public class AbstractDbContext : DbContext
	{
		protected AbstractDbContext([NotNull] string connectionString)
			: base(GetContextOptions(connectionString))
		{

		}

		private static DbContextOptions GetContextOptions([NotNull] string connectionString)
		{
			return new DbContextOptionsBuilder().UseSqlServer(connectionString).Options;
		}
	}
}
