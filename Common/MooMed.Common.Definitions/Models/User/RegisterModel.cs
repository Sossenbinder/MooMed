using System.Runtime.Serialization;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Database.Entities;

namespace MooMed.Common.Definitions.Models.User
{
    [DataContract]
    public class RegisterModel
    {
        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
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
