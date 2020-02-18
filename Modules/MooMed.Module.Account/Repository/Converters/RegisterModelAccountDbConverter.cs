using MooMed.Common.Database.Converter;
using MooMed.Common.Definitions.Database.Entities;
using MooMed.Common.Definitions.Models.User;
using MooMed.Core.Code.Helper.Crypto;

namespace MooMed.Module.Accounts.Repository.Converters
{
	public class RegisterModelAccountDbConverter : IEntityConverter<RegisterModel, AccountEntity>
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
