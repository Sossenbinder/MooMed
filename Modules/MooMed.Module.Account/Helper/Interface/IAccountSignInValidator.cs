using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.Definitions.Models.User.ErrorCodes;

namespace MooMed.Module.Accounts.Helper.Interface
{
    public interface IAccountSignInValidator
    {
        RegistrationValidationResult ValidateRegistrationModel([NotNull] RegisterModel registerModel);

        LoginResponseCode ValidateLoginModel([NotNull] LoginModel loginModel);
    }
}
