using System;
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
        [Key]
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

        [Column("Identifier")]
        public Guid Identifier { get; set; }

        [Column("CashFlowItemType")]
        public CashFlowItemType CashFlowItemType { get; set; }

        [Column("Amount")]
        public double Amount { get; set; }

        [Column("FlowType")]
        public CashFlow FlowType { get; set; }
    }
}