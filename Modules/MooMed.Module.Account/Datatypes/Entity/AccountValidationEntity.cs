using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using MooMed.Common.Definitions.Interface;

namespace MooMed.Module.Accounts.Datatypes.Entity
{
    [Table("AccountEmailValidation")]
    public class AccountValidationEntity : AbstractEntity<int>
    {
        [ForeignKey("AccountEntity")]
        public override int Id { get; set; }

        [NotNull]
        public AccountEntity AccountEntity { get; set; }

        [Key]
        [Column("ValidationGuid")]
        public Guid ValidationGuid { get; set; }

        public override bool Equals(object? obj)
        {
            if (!(obj is AccountValidationEntity comparisonItem))
            {
                return false;
            }

            return Id!.Equals(comparisonItem.Id) && ValidationGuid.Equals(comparisonItem.ValidationGuid);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(
                EqualityComparer<int>.Default.GetHashCode(Id!),
                EqualityComparer<Guid>.Default.GetHashCode(ValidationGuid!));
        }
    }
}