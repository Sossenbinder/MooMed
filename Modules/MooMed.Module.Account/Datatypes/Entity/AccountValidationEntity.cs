using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MooMed.Common.Definitions.Interface;

namespace MooMed.Module.Accounts.Datatypes.Entity
{
    [Table("AccountEmailValidation")]
    public class AccountValidationEntity : IEntity
    {
        [ForeignKey("AccountEntity")]
        [Column("AccountId")]
        public int AccountId { get; set; }

        public AccountEntity AccountEntity { get; set; }

        [Key]
        [Column("ValidationGuid")]
        public Guid ValidationGuid { get; set; }

        public string GetKey() => AccountId.ToString();
    }
}
