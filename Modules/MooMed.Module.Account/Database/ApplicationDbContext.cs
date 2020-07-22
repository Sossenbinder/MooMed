using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MooMed.Common.Definitions.Models.User;

namespace MooMed.Module.Accounts.Database
{
	public class ApplicationDbContext : IdentityDbContext<Account, IdentityRole<int>, int>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Account>()
				.ToTable("Users", "dbo");

			modelBuilder.Entity<Account>()
				.Property(p => p.Id)
				.ValueGeneratedOnAdd();

			modelBuilder.Entity<Account>()
				.Ignore(p => p.ProfilePicturePath);
		}
	}
}
