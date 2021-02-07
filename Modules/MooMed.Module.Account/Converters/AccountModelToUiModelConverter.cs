using MooMed.Common.Database.Converter;
using MooMed.Common.Definitions.Interface;
using MooMed.Common.Definitions.Models.User;
using MooMed.Module.Accounts.Datatypes.UiModels;

namespace MooMed.Module.Accounts.Converters
{
	public class AccountModelToUiModelConverter : IModelToUiModelConverter<Account, AccountUiModel>, IModel
	{
		public AccountUiModel ToUiModel(Account account)
		{
			return new()
			{
				Email = account.Email,
				Id = account.Id,
				UserName = account.UserName
			};
		}
	}
}