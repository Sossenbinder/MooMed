using JetBrains.Annotations;
using ProtoBuf;

namespace MooMed.Common.Definitions.Models.User
{
    [ProtoContract]
    public class RegistrationResult
    {
        [ProtoMember(1)]
        public bool IsSuccess { get; private set; }

        [ProtoMember(2)]
        public RegistrationValidationResult RegistrationValidationResult { get; private set; }

        [ProtoMember(3)]
        public Account Account { get; private set; }

        private RegistrationResult()
        {

        }

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
