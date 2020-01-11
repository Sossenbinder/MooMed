using System.Runtime.Serialization;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Database.Entities;
using ProtoBuf;

namespace MooMed.Common.Definitions.Models.User
{
    [ProtoContract]
    public class RegisterModel
    {
        [ProtoMember(1)]
        public string Email { get; set; }

        [ProtoMember(2)]
        public string UserName { get; set; }

        [ProtoMember(3)]
        public string Password { get; set; }

        [ProtoMember(4)]
        public string ConfirmPassword { get; set; }

        [NotNull]
        public AccountEntity ToAccountEntity()
        {
	        return new AccountEntity()
	        {
		        Email = Email,
		        EmailValidated = false,
		        PasswordHash = "",//TODO,
		        UserName = UserName,
			};
        }
    }
}
