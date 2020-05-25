using System;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.Chat;
using MooMed.Common.Definitions.Notifications;
using MooMed.Common.Definitions.UiModels.Chat;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Core.Code.Logging.Loggers.Interface;
using MooMed.Core.DataTypes;
using MooMed.Eventing.Events.MassTransit.Interface;
using MooMed.Module.Chat.DataTypes.SignalR;
using MooMed.Module.Chat.Service.Interface;

namespace MooMed.ChatService.Service
{
	public class ChatService : Common.ServiceBase.MooMedServiceBase, IChatService
	{
		[NotNull]
		private readonly IMassTransitSignalRBackplaneService _massTransitSignalRBackplaneService;

		[NotNull]
		private readonly IMessageService _messageService;

		public ChatService(
			[NotNull] IMainLogger logger,
			[NotNull] IMassTransitSignalRBackplaneService massTransitSignalRBackplaneService,
			[NotNull] IMessageService messageService) 
			: base(logger)
		{
			_massTransitSignalRBackplaneService = massTransitSignalRBackplaneService;
			_messageService = messageService;
		}

		public async Task<ServiceResponse<RetrievedMessagesModel>> GetMessages(GetMessagesModel getMessagesModel)
		{
			int? toSkip = null;

			if (getMessagesModel.ContinuationToken != null)
			{
				toSkip = int.Parse(getMessagesModel.ContinuationToken);
			}

			var messages = (await _messageService.GetMessages(getMessagesModel.SessionContext, getMessagesModel.ReceiverId, toSkip)).ToList();

			var newContinuationToken = toSkip.HasValue ? toSkip.Value + messages.Count : 0;

			return ServiceResponse<RetrievedMessagesModel>.Success(new RetrievedMessagesModel()
			{
				ContinuationToken = newContinuationToken.ToString(),
				Messages = messages,
			});
		}

		public async Task<ServiceResponse> SendMessage(SendMessageModel sendMessageModel)
		{
			var sessionContext = sendMessageModel.SessionContext;

			var newMessageNotification = new FrontendNotification<ChatMessageUiModel>()
			{
				Data = new ChatMessageUiModel()
				{
					SenderId = sessionContext.Account.Id,
					Message = sendMessageModel.Message,
					Timestamp = sendMessageModel.Timestamp,
				},
				NotificationType = NotificationType.NewChatMessage,
				Operation = Operation.Create
			};

			await _messageService.StoreMessage(sessionContext, new ChatMessageModel()
			{
				Id = Guid.NewGuid(),
				Message = sendMessageModel.Message,
				ReceiverId = sendMessageModel.ReceiverId,
				SenderId = sessionContext.Account.Id,
				Timestamp = sendMessageModel.Timestamp,
			});

			await _massTransitSignalRBackplaneService.RaiseGroupSignalREvent(sendMessageModel.ReceiverId.ToString(), newMessageNotification);

			return ServiceResponse.Success();
		}
	}
}
