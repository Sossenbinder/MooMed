using Microsoft.EntityFrameworkCore;
using MooMed.Common.Database.Context;
using MooMed.Module.Accounts.Datatypes.Entity;

namespace MooMed.Module.Accounts.Database
{
    public class AccountDbContext : AbstractDbContext
    {
        public DbSet<AccountEntity> Account { get; set; }

        public DbSet<AccountValidationEntity> AccountEmailValidation { get; set; }

        public DbSet<FriendsMappingEntity> FriendsMapping { get; set; }

        public AccountDbContext(string connectionString) 
	        : base(connectionString)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
	        base.OnModelCreating(modelBuilder);

	        modelBuilder.Entity<FriendsMappingEntity>()
		        .HasOne(fs => fs.Account)
		        .WithMany(fs => fs.FriendsFrom)
		        .HasForeignKey(fs => fs.AccountId);

	        modelBuilder.Entity<FriendsMappingEntity>()
		        .HasOne(fs => fs.Friend)
		        .WithMany(fs => fs.FriendsTo)
		        .HasForeignKey(fs => fs.FriendId);
        }
    }
}
