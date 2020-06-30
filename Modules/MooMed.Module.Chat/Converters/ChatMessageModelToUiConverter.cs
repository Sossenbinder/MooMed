using MooMed.Common.Database.Converter;
using MooMed.Common.Definitions.Models.Chat;
using MooMed.Common.Definitions.UiModels.Chat;

namespace MooMed.Module.Chat.Converters
{
	public class ChatMessageModelToUiConverter : IModelToUiModelConverter<ChatMessage, ChatMessageUiModel>
	{
		public ChatMessageUiModel ToUiModel(ChatMessage model)
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
