using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.Session.Interface;
using ProtoBuf;

namespace MooMed.Common.Definitions.Models.Chat
{
	[ProtoContract]
	public class GetMessages : SessionContextAttachedContainer
	{
		[ProtoMember(1)]
		public int ReceiverId { get; set; }

		[ProtoMember(2)]
		public string? ContinuationToken { get; set; }
	}
}