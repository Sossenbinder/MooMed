using MooMed.Common.Database.Converter;
using MooMed.Common.Definitions.Models.User;
using MooMed.Core.Code.Helper.Crypto;
using MooMed.Module.Accounts.Datatypes.Entity;

namespace MooMed.Module.Accounts.Repository.Converters
{
	public class RegisterModelAccountDbConverter : IModelToEntityConverter<RegisterModel, AccountEntity, int>
	{
		public AccountEntity ToEntity(RegisterModel model)
		{
			return new AccountEntity()
			{
				Email = model.Email,
				EmailValidated = false,
				PasswordHash = Sha256Helper.Hash(model.Password),
				UserName = model.UserName
			};
		}
	}
}
