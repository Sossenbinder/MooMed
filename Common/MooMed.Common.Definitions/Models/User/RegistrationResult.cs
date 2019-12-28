using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace MooMed.Common.Definitions.Models.User
{
    [DataContract]
    public class RegistrationResult
    {
        [DataMember]
        public bool IsSuccess { get; private set; }

        [DataMember]
        public RegistrationValidationResult RegistrationValidationResult { get; private set; }

        [DataMember]
        public Account Account { get; private set; }

        public RegistrationResult(bool isSuccess, RegistrationValidationResult registrationValidationResult, [CanBeNull] Account account)
        {
            IsSuccess = isSuccess;
            RegistrationValidationResult = registrationValidationResult;
            Account = account;
        }

        [NotNull]
        public static RegistrationResult Success(Account account) => new RegistrationResult(true, RegistrationValidationResult.Success, account);

        [NotNull]
        public static RegistrationResult Failure(RegistrationValidationResult registrationValidationResult) => new RegistrationResult(false, registrationValidationResult, null);
    }
}
