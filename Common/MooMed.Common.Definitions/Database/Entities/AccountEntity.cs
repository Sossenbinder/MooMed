using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Interface;
using MooMed.Common.Definitions.Models.User;

namespace MooMed.Common.Definitions.Database.Entities
{
    [Table("Account")]
    public class AccountEntity : IConvertibleDatabaseEntity<Account>
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [NotNull]
        [Column("UserName")]
        public string UserName { get; set; }

        [NotNull]
        [Column("Email")]
        public string Email { get; set; }

        [Column("EmailValidated")]
        public bool EmailValidated { get; set; }

        [NotNull]
        [Column("PasswordHash")]
        public string PasswordHash { get; set; }

        [Column("LastAccessedAt")]
        public DateTime LastAccessedAt { get; set; }

        [UsedImplicitly]
        // ReSharper disable once NotNullMemberIsNotInitialized
        public AccountEntity()
        {
        }

        public string GetKey() => Id.ToString();

        public Account ToModel()
        {
	        return new Account
	        {
		        Id = Id,
		        Email = Email,
		        UserName = UserName,
		        EmailValidated = EmailValidated,
		        LastAccessedAt = LastAccessedAt
	        };
        }
	}
}
