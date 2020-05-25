using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Interface;

namespace MooMed.Common.Definitions.UiModels.Chat
{
	public class ChatMessageUiModel : IUiModel
	{
		[NotNull]
		public string Message { get; set; }

		public int SenderId { get; set; }

		public DateTime Timestamp { get; set; }
	}
}
