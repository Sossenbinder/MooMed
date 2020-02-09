using Microsoft.EntityFrameworkCore;

namespace MooMed.Common.Database.Context
{
    public abstract class BaseDbContext : DbContext
    {
        protected BaseDbContext()
            : base(GetOptions())
        {

        }

        private static DbContextOptions GetOptions()
        {
            //T|$z6SrDxTG4c&S!kmiG@vS*#b*CWDbR
            return new DbContextOptionsBuilder().UseSqlServer(
				"Server=tcp:moomeddbserver.database.windows.net,1433;" +
				"Initial Catalog=Main;" +
				"Persist Security Info=False;" +
				"User ID=moomedadmin;" +
                "Password=8fC2XaAB1JPPwTL05SoFbdNRvKAH2bHy;" +
				"MultipleActiveResultSets=False;" +
				"Encrypt=True;" +
				"TrustServerCertificate=False;" +
				"Connection Timeout=30;").Options;
        }
    }
}
