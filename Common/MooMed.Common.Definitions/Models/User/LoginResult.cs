using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.User.ErrorCodes;
using ProtoBuf;

namespace MooMed.Common.Definitions.Models.User
{
	[ProtoContract]
	public class LoginResult
	{
		[ProtoMember(1)]
		public IdentityErrorCode IdentityErrorCode { get; set; }

		[ProtoMember(2)]
		public Account Account { get; set; }

		public LoginResult()
		{
		}

		public LoginResult(IdentityErrorCode identityErrorCode, [CanBeNull] Account account = null)
		{
			IdentityErrorCode = identityErrorCode;
			Account = account;
		}
	}
}