using MooMed.Common.Definitions.IPC;
using ProtoBuf;

namespace MooMed.Common.Definitions.Models.User
{
	[ProtoContract]
	public class AddAsFriendModel : SessionContextAttachedContainer
	{
		[ProtoMember(2)]
		public int AccountId { get; set; }
	}
}