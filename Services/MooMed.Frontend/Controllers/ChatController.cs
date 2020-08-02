using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MooMed.Common.Definitions.Models.Chat;
using MooMed.Common.Definitions.UiModels.Chat;
using MooMed.Frontend.Controllers.Base;
using MooMed.Frontend.Controllers.Result;
using MooMed.Grpc.Services.Interface;

namespace MooMed.Frontend.Controllers
{
    public class ChatController : SessionBaseController
    {
	    [NotNull]
	    private readonly IChatService _chatService;

	    public ChatController(
		    [NotNull] ISessionService sessionService,
		    [NotNull] IChatService chatService)
		    : base(sessionService)
        {
	        _chatService = chatService;
        }

        [ItemNotNull]
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GetMessages([NotNull] [FromBody] GetMessagesUiModel model)
        {
	        var messages = await _chatService.GetMessages(new GetMessages()
	        {
		        ReceiverId = model.ReceiverId,
		        SessionContext = CurrentSessionOrFail,
				ContinuationToken = model.ContinuationToken,
	        });

	        return messages.ToJsonResponse();
        }
	}
}