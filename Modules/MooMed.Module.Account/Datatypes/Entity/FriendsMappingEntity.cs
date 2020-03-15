using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MooMed.Common.Definitions.Interface;

namespace MooMed.Module.Accounts.Datatypes.Entity
{
	[Table("FriendsMapping")]
	public class FriendsMappingEntity : IEntity
	{
		[Key]
		[Column("AccountId")]
		public int AccountId { get; set; }

		[NotMapped]
		public AccountEntity Account { get; private set; }

		[Column("FriendId")]
		public int FriendId { get; set; }

		[NotMapped]
		public AccountEntity Friend { get; private set; }

		public string GetKey() => AccountId.ToString();
	}
}
