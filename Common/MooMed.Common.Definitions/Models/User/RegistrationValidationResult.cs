using ProtoBuf;

namespace MooMed.Common.Definitions.Models.User
{
    [ProtoContract]
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
