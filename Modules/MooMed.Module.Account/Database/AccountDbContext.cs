using Microsoft.EntityFrameworkCore;
using MooMed.Common.Definitions.Database.Entities;
using MooMed.Core.Code.Database;

namespace MooMed.Module.Accounts.Database
{
	public class AccountDbContext : BaseDbContext
    {
        public DbSet<AccountEntity> Account { get; set; }

        public DbSet<AccountValidationDbModel> AccountEmailValidation { get; set; }

        public static AccountDbContext Create()
        {
            return new AccountDbContext();
        }
    }
}
