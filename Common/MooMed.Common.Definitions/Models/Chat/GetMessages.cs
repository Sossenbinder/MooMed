using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using ProtoBuf;

namespace MooMed.Common.Definitions.Models.Chat
{
	[ProtoContract]
	public class GetMessages : SessionContextAttachedContainer
	{
		[ProtoMember(2)]
		public int ReceiverId { get; set; }

		[ProtoMember(3)]
		[CanBeNull]
		public string ContinuationToken { get; set; }
	}
}
