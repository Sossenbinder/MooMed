using System;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Interface;
using ProtoBuf;

namespace MooMed.Common.Definitions.Models.Chat
{
	[ProtoContract]
	public class ChatMessageModel : IModel
	{
		[ProtoMember(1)]
		public Guid Id { get; set; }

		[ProtoMember(2)]
		[NotNull]
		public string Message { get; set; }

		[ProtoMember(3)]
		public int SenderId { get; set; }

		[ProtoMember(4)]
		public int ReceiverId { get; set; }

		[ProtoMember(5)]
		public DateTime Timestamp { get; set; }
	}
}
