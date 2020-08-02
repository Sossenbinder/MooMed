using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MooMed.Module.Accounts.Datatypes.Entity;

namespace MooMed.Module.Accounts.Database
{
	public class AccountDbContext : IdentityDbContext<AccountEntity, IdentityRole<int>, int>
	{
		public DbSet<AccountValidationEntity> AccountEmailValidation { get; set; }

		public DbSet<AccountOnlineStateEntity> AccountOnlineState { get; set; }

		public DbSet<FriendsMappingEntity> FriendsMapping { get; set; }

		public AccountDbContext(DbContextOptions<AccountDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<AccountEntity>()
				.ToTable("Users", "dbo");

			modelBuilder.Entity<AccountEntity>()
				.Property(p => p.Id)
				.ValueGeneratedOnAdd();
		}
	}
}