using MooMed.Common.Database.Converter;
using MooMed.Common.Definitions.Models.Chat;
using MooMed.Common.Definitions.UiModels.Chat;

namespace MooMed.Module.Chat.Converters
{
	public class ChatMessageUiConverter : IUiModelConverter<ChatMessageUiModel, ChatMessageModel>
	{
		public ChatMessageUiModel ToUiModel(ChatMessageModel model)
		{
			return new ChatMessageUiModel()
			{
				Message = model.Message,
				SenderId = model.SenderId,
				Timestamp = model.Timestamp,
			};
		}
	}
}
