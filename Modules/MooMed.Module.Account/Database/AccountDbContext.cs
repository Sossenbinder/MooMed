using Microsoft.EntityFrameworkCore;
using MooMed.Common.Database.Context;
using MooMed.Module.Accounts.Datatypes.Entity;

namespace MooMed.Module.Accounts.Database
{
    public class AccountDbContext : AbstractDbContext
    {
        public DbSet<AccountEntity> Account { get; set; }

        public DbSet<AccountValidationEntity> AccountEmailValidation { get; set; }

        public DbSet<AccountOnlineStateEntity> AccountOnlineState { get; set; }

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
		        .WithMany(acc => acc.FriendsFrom)
		        .HasForeignKey(fs => fs.Id);

	        modelBuilder.Entity<FriendsMappingEntity>()
		        .HasOne(fs => fs.Friend)
		        .WithMany(acc => acc.FriendsTo)
		        .HasForeignKey(fs => fs.FriendId);
        }
    }
}
