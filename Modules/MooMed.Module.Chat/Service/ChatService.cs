using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.Chat;
using MooMed.Common.Definitions.Notifications;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Core.Code.Logging.Loggers.Interface;
using MooMed.Eventing.Events.MassTransit.Interface;
using MooMed.Module.Chat.DataTypes.SignalR;

namespace MooMed.Module.Chat.Service
{
	public class ChatService : Common.ServiceBase.MooMedServiceBase, IChatService
	{
		[NotNull]
		private readonly IMassTransitSignalRBackplaneService _massTransitSignalRBackplaneService;

		public ChatService(
			[NotNull] IMainLogger logger,
			[NotNull] IMassTransitSignalRBackplaneService massTransitSignalRBackplaneService) 
			: base(logger)
		{
			_massTransitSignalRBackplaneService = massTransitSignalRBackplaneService;
		}

		public async Task SendMessage(SendMessageModel sendMessageModel)
		{
			var newMessageNotification = new FrontendNotification<NewMessageFrontendNotification>()
			{
				Data = new NewMessageFrontendNotification(sendMessageModel.SessionContext.Account.Id, sendMessageModel.Message),
				NotificationType = NotificationType.NewChatMessage,
				Operation = Operation.Create
			};

			await _massTransitSignalRBackplaneService.RaiseGroupSignalREvent(sendMessageModel.ReceiverId.ToString(), newMessageNotification);
		}
	}
}
