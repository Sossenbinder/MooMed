using System.ComponentModel.DataAnnotations.Schema;
using MooMed.Common.Definitions.Interface;
using MooMed.Common.Definitions.Models.User;

namespace MooMed.Module.Accounts.Datatypes.Entity
{
    public class AccountOnlineStateEntity : AbstractEntity<int>
    {
        public override int Id { get; set; }

        [Column("OnlineState")]
        public AccountOnlineState OnlineState { get; set; }

        [ForeignKey("Id")]
        public AccountEntity Account { get; set; } = null!;
    }
}