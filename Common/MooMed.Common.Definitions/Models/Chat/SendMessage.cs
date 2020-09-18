using System;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.Session.Interface;
using ProtoBuf;

namespace MooMed.Common.Definitions.Models.Chat
{
	[ProtoContract]
	public class SendMessage : SessionContextAttachedContainer
	{
		[ProtoMember(1)]
		public int ReceiverId { get; set; }

		[ProtoMember(2)]
		public string Message { get; set; }

		[ProtoMember(3)]
		public DateTime Timestamp { get; set; }
	}
}