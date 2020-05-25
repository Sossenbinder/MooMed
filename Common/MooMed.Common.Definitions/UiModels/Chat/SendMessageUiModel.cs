using System;

namespace MooMed.Common.Definitions.UiModels.Chat
{
	public class SendMessageUiModel
	{
		public int ReceiverId { get; set; }
		
		public string Message { get; set; }

		public DateTime TimeStamp { get; set; }
	}
}
