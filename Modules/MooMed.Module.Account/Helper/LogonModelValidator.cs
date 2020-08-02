using MooMed.Common.Definitions.Models.User;
using MooMed.Common.Definitions.Models.User.ErrorCodes;
using MooMed.DotNet.Extensions;
using MooMed.Module.Accounts.Helper.Interface;

namespace MooMed.Module.Accounts.Helper
{
	public class LogonModelValidator : ILogonModelValidator
	{
		public IdentityErrorCode ValidateLoginModel(LoginModel loginModel)
		{
			if (loginModel.Email.IsNullOrEmpty())
			{
				return IdentityErrorCode.EmailMissing;
			}

			if (loginModel.Password.IsNullOrEmpty())
			{
				return IdentityErrorCode.PasswordMissing;
			}

			return IdentityErrorCode.None;
		}
	}
}