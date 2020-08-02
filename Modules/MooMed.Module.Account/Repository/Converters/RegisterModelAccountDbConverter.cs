using MooMed.Common.Database.Converter;
using MooMed.Common.Definitions.Models.User;
using MooMed.Encryption;
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
				EmailConfirmed = false,
				PasswordHash = Sha256Helper.Hash(model.Password),
				UserName = model.UserName
			};
		}
	}
}
