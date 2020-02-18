using MooMed.Common.Database.Converter;
using MooMed.Common.Definitions.Database.Entities;
using MooMed.Common.Definitions.Models.User;

namespace MooMed.Module.Accounts.Repository.Converters
{
	public class AccountDbConverter : IBiDirectionalDbConverter<Account, AccountEntity>
	{
		public AccountEntity ToEntity(Account model)
		{
			return new AccountEntity()
			{
				Email = model.Email,
				EmailValidated = false,
			};
		}

		public Account ToModel(AccountEntity entity)
		{
			return new Account()
			{
				Email = entity.Email,
				EmailValidated = entity.EmailValidated,
				Id = entity.Id,
				LastAccessedAt = entity.LastAccessedAt,
				UserName = entity.UserName,
			};
		}
	}
}
