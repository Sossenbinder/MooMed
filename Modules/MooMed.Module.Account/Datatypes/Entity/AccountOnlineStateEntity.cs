using System.ComponentModel.DataAnnotations.Schema;
using MooMed.Common.Definitions.Interface;
using MooMed.Common.Definitions.Models.User;

namespace MooMed.Module.Accounts.Datatypes.Entity
{
	public class AccountOnlineStateEntity : IEntity<int>
	{
		public int Id { get; set; }

		[Column("OnlineState")]
		public AccountOnlineState OnlineState { get; set; }

		[ForeignKey("Id")]
		public AccountEntity Account { get; set; }
	}
}
