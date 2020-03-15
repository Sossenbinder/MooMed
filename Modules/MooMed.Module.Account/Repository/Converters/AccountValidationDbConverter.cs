using MooMed.Common.Database.Converter;
using MooMed.Common.Definitions.Models.User;
using MooMed.Module.Accounts.Datatypes.Entity;

namespace MooMed.Module.Accounts.Repository.Converters
{
	public class AccountValidationDbConverter : IBiDirectionalDbConverter<AccountValidation, AccountValidationEntity>
	{
		public AccountValidationEntity ToEntity(AccountValidation model)
		{
			return new AccountValidationEntity()
			{
				AccountId = model.AccountId,
				ValidationGuid = model.ValidationGuid,
			};
		}

		public AccountValidation ToModel(AccountValidationEntity entity)
		{
			return new AccountValidation()
			{
				AccountId = entity.AccountId,
				ValidationGuid = entity.ValidationGuid,
			};
		}
	}
}
