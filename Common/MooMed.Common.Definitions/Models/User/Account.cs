using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Database.Entities;
using MooMed.Common.Definitions.Interface;

namespace MooMed.Common.Definitions.Models.User
{
    [DataContract]
    public class Account : IEntityConvertibleModel<AccountEntity>
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public bool EmailValidated { get; set; }

        [DataMember]
        public DateTime LastAccessedAt { get; set; }

        [DataMember]
        public string ProfilePicturePath { get; set; }

        public AccountEntity ToEntity()
        {
			return new AccountEntity
			{
				Id = Id,
				Email = Email,
				UserName = UserName,
				EmailValidated = EmailValidated,
				LastAccessedAt = LastAccessedAt
			};

		}
    }

    public static class AccountExtensions
    {
        [NotNull]
        public static string IdAsKey([NotNull] this Account account)
        {
            return $"a-{account.Id}";
        }
    }
}
