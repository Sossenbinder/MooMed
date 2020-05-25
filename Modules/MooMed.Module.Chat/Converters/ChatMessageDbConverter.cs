using System;
using JetBrains.Annotations;
using MooMed.Common.Database.Converter;
using MooMed.Common.Definitions.Models.Chat;
using MooMed.Module.Chat.DataTypes.Entity;
using MooMed.Module.Chat.Helper;

namespace MooMed.Module.Chat.Converters
{
	public class ChatMessageDbConverter : IBiDirectionalDbConverter<ChatMessageModel, ChatMessageEntity, Guid>
	{
		[NotNull]
		private readonly ChatMessageEncodingHelper _chatMessageEncodingHelper;

		public ChatMessageDbConverter([NotNull] ChatMessageEncodingHelper chatMessageEncodingHelper)
		{
			_chatMessageEncodingHelper = chatMessageEncodingHelper;
		}

		public ChatMessageModel ToModel(ChatMessageEntity entity)
		{
			var timestamp = entity.Timestamp.ToLocalTime();

			return new ChatMessageModel()
			{
				Id = entity.Id,
				Message = _chatMessageEncodingHelper.Decode(entity.Message),
				ReceiverId = entity.ReceiverId,
				SenderId = entity.SenderId,
				Timestamp = timestamp,
			};
		}

		public ChatMessageEntity ToEntity(ChatMessageModel model)
		{
			var timestamp = model.Timestamp.ToUniversalTime();

			return new ChatMessageEntity()
			{
				Id = model.Id,
				Message = _chatMessageEncodingHelper.Encode(model.Message),
				ReceiverId = model.ReceiverId,
				SenderId = model.SenderId,
				Timestamp = timestamp,
			};
		}
	}
}
