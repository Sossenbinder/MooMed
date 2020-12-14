using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace MooMed.Module.Accounts.Database
{
    public class AccountDbContextFactory : IDbContextFactory<AccountDbContext>
    {
        [NotNull]
        private readonly DbContextOptions<AccountDbContext> _options;

        public AccountDbContextFactory([NotNull] DbContextOptions<AccountDbContext> options)
        {
            _options = options;
        }

        public AccountDbContext CreateDbContext()
        {
            return new(_options);
        }
    }
}