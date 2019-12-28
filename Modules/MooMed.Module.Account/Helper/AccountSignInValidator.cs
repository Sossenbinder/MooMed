using System.Text.RegularExpressions;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.Definitions.Models.User.ErrorCodes;
using MooMed.Core.Code.Extensions;
using MooMed.Module.Accounts.Helper.Interface;

namespace MooMed.Module.Accounts.Helper
{
    public class AccountSignInValidator : IAccountSignInValidator
    {
        public RegistrationValidationResult ValidateRegistrationModel(RegisterModel registerModel)
        {
            if (registerModel.Email.IsNullOrEmpty())
            {
                return RegistrationValidationResult.EmailNullOrEmpty;
            }

            if (registerModel.Password.IsNullOrEmpty())
            {
                return RegistrationValidationResult.PasswordNullOrEmpty;
            }

            if (registerModel.UserName.IsNullOrEmpty())
            {
                return RegistrationValidationResult.UserNameNullOrEmpty;
            }

            if (!registerModel.Password.Equals(registerModel.ConfirmPassword))
            {
                return RegistrationValidationResult.PasswordsNotMatching;
            }

            if (!Regex.IsMatch(registerModel.Email, @"^\S+@\S+$"))
            {
                return RegistrationValidationResult.EmailInvalid;
            }


            return RegistrationValidationResult.Success;
        }

        public LoginResponseCode ValidateLoginModel(LoginModel loginModel)
        {

            if (loginModel.Email.IsNullOrEmpty())
            {
                return LoginResponseCode.EmailNullOrEmpty;
            }

            if (loginModel.Password.IsNullOrEmpty())
            {
                return LoginResponseCode.PasswordNullOrEmpty;
            }

            return LoginResponseCode.Success;
        }
    }
}
