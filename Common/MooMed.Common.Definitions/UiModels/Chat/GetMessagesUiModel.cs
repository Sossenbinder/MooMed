using System;
using System.Collections.Generic;
using System.Text;
using MooMed.Common.Definitions.Interface;

namespace MooMed.Common.Definitions.UiModels.Chat
{
	public class GetMessagesUiModel : IUiModel
	{
		public int ReceiverId { get; set; }

		public string ContinuationToken { get; set; }
	}
}
