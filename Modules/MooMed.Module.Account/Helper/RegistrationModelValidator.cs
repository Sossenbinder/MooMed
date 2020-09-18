using System.Text.RegularExpressions;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.Definitions.Models.User.ErrorCodes;
using MooMed.DotNet.Extensions;
using MooMed.Module.Accounts.Helper.Interface;

namespace MooMed.Module.Accounts.Helper
{
	public class RegistrationModelValidator : IRegistrationModelValidator
	{
		public IdentityErrorCode ValidateRegistrationModel(RegisterModel registerModel)
		{
			if (registerModel.Email.IsNullOrEmpty())
			{
				return IdentityErrorCode.EmailMissing;
			}

			if (registerModel.Password.IsNullOrEmpty())
			{
				return IdentityErrorCode.PasswordMismatch;
			}

			if (registerModel.UserName.IsNullOrEmpty())
			{
				return IdentityErrorCode.UserNameNullOrEmpty;
			}

			if (!registerModel.Password.Equals(registerModel.ConfirmPassword))
			{
				return IdentityErrorCode.PasswordMismatch;
			}

			if (!Regex.IsMatch(registerModel.Email, @"^\S+@\S+$"))
			{
				return IdentityErrorCode.InvalidEmail;
			}

			return IdentityErrorCode.Success;
		}
	}
}