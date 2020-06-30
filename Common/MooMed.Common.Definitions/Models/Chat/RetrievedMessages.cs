using System.Collections.Generic;
using ProtoBuf;

namespace MooMed.Common.Definitions.Models.Chat
{
	[ProtoContract]
	public class RetrievedMessages
	{
		[ProtoMember(1)]
		public List<ChatMessage> Messages { get; set; }

		[ProtoMember(2)]
		public string ContinuationToken { get; set; }
	}
}
