using System;
using MooMed.Common.Definitions.Interface;
using MooMed.Common.Definitions.IPC;
using ProtoBuf;

namespace MooMed.Common.Definitions.Models.Chat
{
	[ProtoContract]
	public class SendMessageModel : SessionContextAttachedContainer, IModel
	{
		[ProtoMember(2)]
		public int ReceiverId { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public DateTime Timestamp { get; set; }
	}
}
