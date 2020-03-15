using MooMed.Common.Definitions.IPC;
using ProtoBuf;

namespace MooMed.Common.Definitions.Messages.Account
{
	[ProtoContract]
	public class AddAsFriendMessage : SessionContextAttachedContainer
	{
		[ProtoMember(2)]
		public int AccountId { get; set; }
	}
}
