using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MooMed.Common.Definitions.Messages.Account;
using MooMed.Common.Definitions.UiModels.Friends;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Web.Controllers.Base;
using MooMed.Web.Controllers.Result;

namespace MooMed.Web.Controllers
{
	public class FriendsController : SessionBaseController
	{
		[NotNull]
		private readonly IAccountService _accountService;

		public FriendsController(
			[NotNull] ISessionService sessionService,
			[NotNull] IAccountService accountService) 
			: base(sessionService)
		{
			_accountService = accountService;
		}

		[ItemNotNull]
		[HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> GetFriends()
		{
			var getFriendsResponse = await _accountService.GetFriends(CurrentSessionOrFail);

			return getFriendsResponse.ToJsonResponse();
		}

		[ItemNotNull]
		[HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> AddFriend([NotNull] [FromBody] AddFriendUiModel addFriendUiModel)
		{
			var message = new AddAsFriendMessage()
			{
				SessionContext = CurrentSession,
				AccountId = addFriendUiModel.AccountId,
			};

			var friendAddResponse = await _accountService.AddAsFriend(message);

			return friendAddResponse.ToJsonResponse();
		}
	}
}
