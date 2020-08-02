using MooMed.Common.Database.Converter;
using MooMed.Common.Definitions.Models.User;
using MooMed.Module.Accounts.Datatypes.Entity;

namespace MooMed.Module.Accounts.Repository.Converters
{
	public class AccountDbConverter : IBiDirectionalDbConverter<Account, AccountEntity, int>
	{
		public AccountEntity ToEntity(Account model)
		{
			return new AccountEntity
			{
				Id = model.Id,
				Email = model.Email,
				UserName = model.UserName,
				EmailConfirmed = model.EmailConfirmed,
				LastAccessedAt = model.LastAccessedAt,
				AccessFailedCount = model.AccessFailedCount,
				ConcurrencyStamp = model.ConcurrencyStamp,
				LockoutEnabled = model.LockoutEnabled,
				LockoutEnd = model.LockoutEnd,
				NormalizedEmail = model.NormalizedEmail,
				NormalizedUserName = model.NormalizedUserName,
				PasswordHash = model.PasswordHash,
				PhoneNumber = model.PhoneNumber,
				PhoneNumberConfirmed = model.PhoneNumberConfirmed,
				SecurityStamp = model.SecurityStamp,
				TwoFactorEnabled = model.TwoFactorEnabled
			};
		}

		public Account ToModel(AccountEntity entity)
		{
			return new Account
			{
				Id = entity.Id,
				Email = entity.Email,
				UserName = entity.UserName,
				EmailConfirmed = entity.EmailConfirmed,
				LastAccessedAt = entity.LastAccessedAt,
				AccessFailedCount = entity.AccessFailedCount,
				ConcurrencyStamp = entity.ConcurrencyStamp,
				LockoutEnabled = entity.LockoutEnabled,
				LockoutEnd = entity.LockoutEnd,
				NormalizedEmail = entity.NormalizedEmail,
				NormalizedUserName = entity.NormalizedUserName,
				PasswordHash = entity.PasswordHash,
				PhoneNumber = entity.PhoneNumber,
				PhoneNumberConfirmed = entity.PhoneNumberConfirmed,
				SecurityStamp = entity.SecurityStamp,
				TwoFactorEnabled = entity.TwoFactorEnabled
			};
		}
	}
}
