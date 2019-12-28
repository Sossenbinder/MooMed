using Microsoft.EntityFrameworkCore;

namespace MooMed.Core.Code.Database
{
    public abstract class BaseDbContext : DbContext
    {
        protected BaseDbContext()
            : base(GetOptions())
        {

        }

        private static DbContextOptions GetOptions()
        {
            return new DbContextOptionsBuilder().UseSqlServer(
				"Server=tcp:moomeddbserver.database.windows.net,1433;" +
				"Initial Catalog=Main;" +
				"Persist Security Info=False;" +
				"User ID=moomedadmin;" +
				"Password=T|$z6SrDxTG4c&S!kmiG@vS*#b*CWDbR;" +
				"MultipleActiveResultSets=False;" +
				"Encrypt=True;" +
				"TrustServerCertificate=False;" +
				"Connection Timeout=30;").Options;
        }
    }
}
