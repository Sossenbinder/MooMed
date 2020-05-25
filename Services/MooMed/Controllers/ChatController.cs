using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MooMed.Common.Definitions.Models.Chat;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Web.Controllers.Base;
using MooMed.Web.Controllers.Result;

namespace MooMed.Web.Controllers
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
        public async Task<ActionResult> GetMessages(int receiverId, [CanBeNull] string continuationToken = null)
        {
	        var messages = await _chatService.GetMessages(new GetMessagesModel()
	        {
		        ReceiverId = receiverId,
		        SessionContext = CurrentSessionOrFail,
				ContinuationToken = continuationToken,
	        });

	        return messages.ToJsonResponse();
        }
	}
}