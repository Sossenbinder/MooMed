using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.User.ErrorCodes;
using ProtoBuf;

namespace MooMed.Common.Definitions.Models.User
{
    [ProtoContract]
    public class LoginResult
    {
	    [ProtoMember(1)]
        public LoginResponseCode LoginResponseCode { get; set; }

        [ProtoMember(2)]
        public Account Account { get; set; }

        public LoginResult()
        {

        }

        public LoginResult(LoginResponseCode loginResponseCode, [CanBeNull] Account account = null)
        {
            LoginResponseCode = loginResponseCode;
            Account = account;
        }
    }
}
