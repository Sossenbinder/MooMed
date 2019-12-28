namespace MooMed.Common.Definitions.Models.User
{
    public enum RegistrationValidationResult
    {
        None,
        Success,
        EmailInvalid,
        EmailTaken,
        EmailNullOrEmpty,
        PasswordNullOrEmpty,
        UserNameTaken,
        UserNameNullOrEmpty,
        PasswordsNotMatching,
    }
}
