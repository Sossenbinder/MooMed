using MooMed.Common.Definitions.IPC;
using ProtoBuf;

namespace MooMed.Common.Definitions.Models.Chat
{
	[ProtoContract]
	public class SendMessageModel : SessionContextAttachedContainer
	{
		[ProtoMember(2)]
		public int ReceiverId { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }
	}
}
