using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Interface;
using MooMed.Common.Definitions.Models.Saving;
using MooMed.Module.Accounts.Datatypes.Entity;

namespace MooMed.Module.Saving.Database.Entities
{
    [Table("CashFlowItems")]
    public class CashFlowItemEntity : IEntity<int>
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Id))]
        public AccountEntity? Account
        {
            get;
            [UsedImplicitly]
            private set;
        }

        [Column("Name")]
        public string Name { get; set; } = null!;

        [Key]
        [Column("Identifier")]
        public Guid Identifier { get; set; }

        [Column("CashFlowItemType")]
        public CashFlowItemType CashFlowItemType { get; set; }

        [Column("Amount")]
        public double Amount { get; set; }

        [Column("FlowType")]
        public CashFlow FlowType { get; set; }

        public override bool Equals(object? obj)
        {
            if (!(obj is CashFlowItemEntity comparisonItem))
            {
                return false;
            }

            return Id!.Equals(comparisonItem.Id) && Identifier.Equals(comparisonItem.Identifier);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(
                EqualityComparer<int>.Default.GetHashCode(Id!),
                EqualityComparer<Guid>.Default.GetHashCode(Identifier));
        }
    }
}