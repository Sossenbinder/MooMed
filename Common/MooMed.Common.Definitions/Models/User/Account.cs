using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Database.Entities;
using MooMed.Common.Definitions.Interface;
using ProtoBuf;

namespace MooMed.Common.Definitions.Models.User
{
    [ProtoContract]
    public class Account : IModel
    {
        [ProtoMember(1)]
        public int Id { get; set; }

        [ProtoMember(2)]
        public string UserName { get; set; }

        [ProtoMember(3)]
        public string Email { get; set; }

        [ProtoMember(4)]
        public bool EmailValidated { get; set; }

        [ProtoMember(5)]
        public DateTime LastAccessedAt { get; set; }

        [ProtoMember(6)]
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
