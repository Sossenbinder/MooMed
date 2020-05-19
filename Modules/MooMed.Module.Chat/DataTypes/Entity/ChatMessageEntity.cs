using System;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Interface;
using MooMed.Module.Accounts.Datatypes.Entity;

namespace MooMed.Module.Chat.DataTypes.Entity
{
	public class ChatMessageEntity : IEntity<Guid>
	{
		public Guid Id { get; set; }

		[NotNull]
		[Column("Message")]
		public string Message { get; set; }

		[Column("SenderId")]
		public int SenderId { get; set; }
		
		[Column("ReceiverId")]
		public int ReceiverId { get; set; }
		

		[NotNull]
		[ForeignKey("SenderId")]
		public AccountEntity Sender { get; set; }

		[NotNull]
		[ForeignKey("Receiver")]
		public AccountEntity Receiver { get; set; }
	}
}
