using System.Runtime.Serialization;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.User.ErrorCodes;

namespace MooMed.Common.Definitions.Models.User
{
    [DataContract]
    public class LoginResult
    {
        [DataMember]
        public LoginResponseCode LoginResponseCode { get; private set; }

        [DataMember]
        public Account Account { get; private set; }

        public LoginResult(LoginResponseCode loginResponseCode, [CanBeNull] Account account)
        {
            LoginResponseCode = loginResponseCode;
            Account = account;
        }
    }
}
