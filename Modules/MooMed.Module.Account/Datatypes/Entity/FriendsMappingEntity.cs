using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Interface;

namespace MooMed.Module.Accounts.Datatypes.Entity
{
	[Table("FriendsMapping")]
	public class FriendsMappingEntity : IEntity<int>
	{
		public int Id { get; set; }

		[ForeignKey("Id")]
		public AccountEntity Account { 
			get; 
			[UsedImplicitly] private set;
		}

		[Key]
		[Column("FriendId")]
		public int FriendId { get; set; }

		[ForeignKey("FriendId")]
		public AccountEntity Friend
		{
			get; 
			[UsedImplicitly] private set;
		}
	}
}
