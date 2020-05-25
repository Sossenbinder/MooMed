using MooMed.Common.Database.Converter;
using MooMed.Common.Definitions.Models.Chat;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Module.Chat.DataTypes.Entity;
using MooMed.Module.Chat.Repository.Interface;
using MooMed.Module.Chat.Service.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace MooMed.Module.Chat.Service
{
	public class MessageService : IMessageService
	{
		[NotNull]
		private readonly IChatMessageRepository _chatMessageRepository;

		[NotNull]
		private readonly IBiDirectionalDbConverter<ChatMessageModel, ChatMessageEntity, Guid> _chatMessageConverter;

		public MessageService(
			[NotNull] IChatMessageRepository chatMessageRepository,
			[NotNull] IBiDirectionalDbConverter<ChatMessageModel, ChatMessageEntity, Guid> chatMessageConverter)
		{
			_chatMessageRepository = chatMessageRepository;
			_chatMessageConverter = chatMessageConverter;
		}

		public async Task<IEnumerable<ChatMessageModel>> GetMessages(ISessionContext sessionContext, int receiverId, int? continuationToken = null)
		{
			var accountId = sessionContext.Account.Id;

			var messages = await _chatMessageRepository.GetChatMessages(
				message => message.ReceiverId == receiverId && message.SenderId == accountId ||
					message.ReceiverId == accountId && message.SenderId == receiverId,
				continuationToken);

			var chatMessageModels = messages.ToList().ConvertAll(x => _chatMessageConverter.ToModel(x));

			return chatMessageModels;
		}

		public async Task StoreMessage(ISessionContext sessionContext, ChatMessageModel messageModel)
		{
			await _chatMessageRepository.Create(_chatMessageConverter.ToEntity(messageModel));
		}
	}
}