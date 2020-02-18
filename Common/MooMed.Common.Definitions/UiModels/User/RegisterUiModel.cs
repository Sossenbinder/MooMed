using MooMed.Common.Definitions.Interface;
using MooMed.Common.Definitions.Models.User;

namespace MooMed.Common.Definitions.UiModels.User
{
	public class RegisterUiModel : IUiModel
	{
		public string Email { get; set; }

		public string UserName { get; set; }

		public string Password { get; set; }

		public string ConfirmPassword { get; set; }

		public RegisterModel ToModel()
		{
			return new RegisterModel()
			{
				ConfirmPassword = ConfirmPassword,
				Email = Email,
				Password = Password,
				UserName = UserName
			};
		}
	}
}
