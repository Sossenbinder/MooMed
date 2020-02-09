using Microsoft.EntityFrameworkCore;
using MooMed.Common.Database.Context;
using MooMed.Common.Definitions.Database.Entities;

namespace MooMed.Module.Accounts.Database
{
    public class AccountDbContext : AbstractDbContext
    {
        public DbSet<AccountEntity> Account { get; set; }

        public DbSet<AccountValidationDbModel> AccountEmailValidation { get; set; }

        public AccountDbContext(string connectionString) 
	        : base(connectionString)
        {
        }
    }
}
