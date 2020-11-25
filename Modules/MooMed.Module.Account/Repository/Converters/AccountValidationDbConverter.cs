using MooMed.Common.Database.Converter;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;
using MooMed.Module.Accounts.Datatypes.Entity;

namespace MooMed.Module.Accounts.Repository.Converters
{
    public class AccountValidationDbConverter : IBiDirectionalDbConverter<AccountValidation, AccountValidationEntity, int>
    {
        public AccountValidationEntity ToEntity(AccountValidation model, ISessionContext sessionContext = null!)
        {
            return new AccountValidationEntity()
            {
                Id = model.AccountId,
                ValidationGuid = model.ValidationGuid,
            };
        }

        public AccountValidation ToModel(AccountValidationEntity entity)
        {
            return new AccountValidation()
            {
                AccountId = entity.Id,
                ValidationGuid = entity.ValidationGuid,
            };
        }
    }
}