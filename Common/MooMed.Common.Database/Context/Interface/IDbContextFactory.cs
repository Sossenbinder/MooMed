namespace MooMed.Common.Database.Context.Interface
{
	public interface IDbContextFactory<out TDbContext>
		where TDbContext : AbstractDbContext
	{
		TDbContext CreateContext();
	}
}
