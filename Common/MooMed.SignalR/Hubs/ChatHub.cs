using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.Chat;
using MooMed.Common.Definitions.UiModels.Chat;
using MooMed.Common.ServiceBase.Interface;
using MooMed.SignalR.Hubs.Base;

namespace MooMed.SignalR.Hubs
{
	public partial class SignalRHub : SessionBaseHub
	{
		[NotNull]
		private readonly IChatService _chatService;

		public SignalRHub(
			[NotNull] IChatService chatService,
			[NotNull] ISessionService sessionService)
			: base(sessionService)
		{
			_chatService = chatService;
		}

		[UsedImplicitly]
		public async Task<bool> SendMessage([NotNull] SendMessageUiModel sendMessageModel)
		{
			var sessionContext = await GetSessionContextOrFail();

			var response = await _chatService.SendMessage(new SendMessage()
			{
				Message = sendMessageModel.Message,
				ReceiverId = sendMessageModel.ReceiverId,
				Timestamp = sendMessageModel.TimeStamp,
				SessionContext = sessionContext
			});

			return response.IsSuccess;
		}
	}
}
