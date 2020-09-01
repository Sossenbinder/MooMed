using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;

namespace MooMed.Common.Database.Context
{
	public class AbstractDbContext : DbContext
	{
		private static readonly LoggerFactory LoggerFactory = new LoggerFactory(new[] {
			new DebugLoggerProvider()
		});

		protected AbstractDbContext([NotNull] string connectionString)
			: base(GetContextOptions(connectionString))
		{
		}

		private static DbContextOptions GetContextOptions([NotNull] string connectionString)
		{
			return new DbContextOptionsBuilder().UseSqlServer(connectionString).Options;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseLoggerFactory(LoggerFactory);
		}
	}
}