using Microsoft.EntityFrameworkCore;

namespace MooMed.Common.Database.Context.Interface
{
	public interface IDbContextFactory<out TDbContext>
		where TDbContext : DbContext
	{
		TDbContext CreateContext();
	}
}
