using System.Collections.Generic;
using ProtoBuf;

namespace MooMed.Common.Definitions.Models.Chat
{
	[ProtoContract]
	public class RetrievedMessagesModel
	{
		[ProtoMember(1)]
		public List<ChatMessageModel> Messages { get; set; }

		[ProtoMember(2)]
		public string ContinuationToken { get; set; }
	}
}
