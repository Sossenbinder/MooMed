using System;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.Chat;
using MooMed.Common.Definitions.Notifications;
using MooMed.Common.Definitions.UiModels.Chat;
using MooMed.Common.ServiceBase.ServiceBase;
using MooMed.Core.DataTypes;
using MooMed.Eventing.Events.MassTransit.Interface;
using MooMed.Eventing.Helper;
using MooMed.Logging.Abstractions.Interface;
using MooMed.Module.Chat.Service.Interface;
using MooMed.ServiceBase.Services.Interface;

namespace MooMed.ChatService.Service
{
    public class ChatService : MooMedServiceBaseWithLogger, IChatService
    {
        [NotNull]
        private readonly IMassTransitSignalRBackplaneService _massTransitSignalRBackplaneService;

        [NotNull]
        private readonly IMessageService _messageService;

        public ChatService(
            [NotNull] IMooMedLogger logger,
            [NotNull] IMassTransitSignalRBackplaneService massTransitSignalRBackplaneService,
            [NotNull] IMessageService messageService)
            : base(logger)
        {
            _massTransitSignalRBackplaneService = massTransitSignalRBackplaneService;
            _messageService = messageService;
        }

        public async Task<ServiceResponse<RetrievedMessages>> GetMessages(GetMessages getMessages)
        {
            int? toSkip = null;

            if (getMessages.ContinuationToken != null)
            {
                toSkip = int.Parse(getMessages.ContinuationToken);
            }

            var messages = (await _messageService.GetMessages(getMessages.SessionContext, getMessages.ReceiverId, toSkip)).ToList();

            var newContinuationToken = toSkip.HasValue ? toSkip.Value + messages.Count : 0;

            return ServiceResponse<RetrievedMessages>.Success(new RetrievedMessages()
            {
                ContinuationToken = newContinuationToken.ToString(),
                Messages = messages,
            });
        }

        public async Task<ServiceResponse> SendMessage(SendMessage sendMessage)
        {
            var sessionContext = sendMessage.SessionContext;

            await _messageService.StoreMessage(sessionContext, new ChatMessage()
            {
                Id = Guid.NewGuid(),
                Message = sendMessage.Message,
                ReceiverId = sendMessage.ReceiverId,
                SenderId = sessionContext.Account.Id,
                Timestamp = sendMessage.Timestamp,
            });

            var newMessageNotification = FrontendNotificationFactory.Create(new ChatMessageUiModel()
            {
                SenderId = sessionContext.Account.Id,
                Message = sendMessage.Message,
                Timestamp = sendMessage.Timestamp,
            }, NotificationType.NewChatMessage);

            await _massTransitSignalRBackplaneService.RaiseGroupSignalREvent(sendMessage.ReceiverId.ToString(), newMessageNotification);

            return ServiceResponse.Success();
        }
    }
}