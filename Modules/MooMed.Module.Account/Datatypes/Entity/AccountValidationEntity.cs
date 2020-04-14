using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using MooMed.Common.Definitions.Interface;

namespace MooMed.Module.Accounts.Datatypes.Entity
{
    [Table("AccountEmailValidation")]
    public class AccountValidationEntity : IEntity<int>
    {
        [ForeignKey("AccountEntity")]
        public int Id { get; set; }

        [NotNull]
        public AccountEntity AccountEntity { get; set; }

        [Key]
        [Column("ValidationGuid")]
        public Guid ValidationGuid { get; set; }
    }
}
