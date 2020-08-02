using MooMed.Common.Database.Converter;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.Definitions.UiModels.User;

namespace MooMed.Module.AccountValidation.Converters
{
	public class AccountValidationUiModelConverter : IUiModelToModelConverter<AccountValidationUiModel, AccountValidationModel>
	{
		public AccountValidationModel ToModel(AccountValidationUiModel uiModel)
		{
			return new AccountValidationModel()
			{
				AccountId = uiModel.AccountId,
				Token = uiModel.Token,
			};
		}
	}
}
