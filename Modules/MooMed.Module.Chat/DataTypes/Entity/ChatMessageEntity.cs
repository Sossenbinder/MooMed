using System;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Interface;

namespace MooMed.Module.Chat.DataTypes.Entity
{
    [Table("Messages")]
    public class ChatMessageEntity : AbstractEntity<Guid>
    {
        public override Guid Id { get; set; }

        [NotNull]
        [Column("Message")]
        public string? Message { get; set; }

        [Column("SenderId")]
        public int SenderId { get; set; }

        [Column("ReceiverId")]
        public int ReceiverId { get; set; }

        [Column("Timestamp")]
        public DateTime Timestamp { get; set; }
    }
}