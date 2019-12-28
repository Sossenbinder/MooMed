using JetBrains.Annotations;
using MooMed.Core.Code.Database;

namespace MooMed.Web.Database
{
    public class ApplicationDbContext : BaseDbContext
    {
        [NotNull]
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}