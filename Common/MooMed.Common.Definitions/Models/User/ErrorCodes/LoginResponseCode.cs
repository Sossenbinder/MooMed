using System.Runtime.Serialization;

namespace MooMed.Common.Definitions.Models.User.ErrorCodes
{
	[DataContract]
    public enum LoginResponseCode
    {
		[EnumMember]
        None,

        [EnumMember]
		Success,

		[EnumMember]
		EmailNullOrEmpty,

		[EnumMember]
		PasswordNullOrEmpty,

		[EnumMember]
		EmailNotValidated,

		[EnumMember]
		AccountNotFound,

		[EnumMember]
		PasswordWrong,

		[EnumMember]
		UnknownFailure,
    }
}
