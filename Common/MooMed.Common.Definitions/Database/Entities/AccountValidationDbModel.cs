using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.User;

namespace MooMed.Common.Definitions.Database.Entities
{
    [Table("AccountEmailValidation")]
    public class AccountValidationDbModel
    {
        [ForeignKey("AccountDbModel")]
        [Column("AccountId")]
        public int AccountId { get; set; }

        public virtual AccountEntity AccountEntity { get; set; }

        [Key]
        [Column("ValidationGuid")]
        public Guid ValidationGuid { get; set; }
    }

    public static class AccountEmailValidationDbModelConverter
    {
        [NotNull]
        public static AccountValidation ToModel([NotNull] this AccountValidationDbModel accountEmailValidationDbModel)
        {
            return new AccountValidation
            {
                AccountId = accountEmailValidationDbModel.AccountId,
                ValidationGuid = accountEmailValidationDbModel.ValidationGuid
            };
        }
    }
}
