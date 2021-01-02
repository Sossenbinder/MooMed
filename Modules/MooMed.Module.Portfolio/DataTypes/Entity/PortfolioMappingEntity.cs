using JetBrains.Annotations;
using MooMed.Common.Definitions.Interface;
using MooMed.Module.Accounts.Datatypes.Entity;
using MooMed.Module.Finance.Database.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MooMed.Module.Portfolio.DataTypes.Entity
{
    [Table("PortfolioMapping")]
    public class PortfolioMappingEntity : AbstractEntity<int>
    {
        public override int Id { get; set; }

        [Column("Isin")]
        public string Isin { get; set; }

        [Column("Amount")]
        public float Amount { get; set; }

        [ForeignKey("Id")]
        public AccountEntity Account
        {
            get;
            [UsedImplicitly]
            private set;
        }

        [ForeignKey("Isin")]
        public ExchangeTradedEntity ExchangeTraded
        {
            get;
            [UsedImplicitly]
            private set;
        }
    }
}