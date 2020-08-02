using System.Collections.Generic;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.User.ErrorCodes;
using ProtoBuf;

namespace MooMed.Common.Definitions.Models.User
{
	[ProtoContract]
	public class RegistrationResult
	{
		[ProtoMember(1)]
		public bool IsSuccess { get; }

		[ProtoMember(2)]
		public IEnumerable<IdentityErrorCode> IdentityErrorCodes { get; }

		[ProtoMember(3)]
		public Account Account { get; }

		private RegistrationResult()
		{
		}

		public RegistrationResult(bool isSuccess, IdentityErrorCode identityErrorCode, [CanBeNull] Account account)
			: this(isSuccess, new List<IdentityErrorCode> { identityErrorCode }, account)

		{
		}

		public RegistrationResult(bool isSuccess, IEnumerable<IdentityErrorCode> identityErrorCodes, [CanBeNull] Account account)
		{
			IsSuccess = isSuccess;
			IdentityErrorCodes = identityErrorCodes;
			Account = account;
		}

		[NotNull]
		public static RegistrationResult Success(Account account) => new RegistrationResult(true, IdentityErrorCode.None, account);

		[NotNull]
		public static RegistrationResult Failure(IdentityErrorCode errorCode) => new RegistrationResult(false, new List<IdentityErrorCode>() { errorCode }, null);

		[NotNull]
		public static RegistrationResult Failure(IEnumerable<IdentityErrorCode> errorCodes) => new RegistrationResult(false, errorCodes, null);
	}
}