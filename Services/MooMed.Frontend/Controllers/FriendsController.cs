using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.Definitions.UiModels.Friends;
using MooMed.Frontend.Controllers.Base;
using MooMed.Frontend.Controllers.Result;
using MooMed.ServiceBase.Services.Interface;

namespace MooMed.Frontend.Controllers
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
		public async Task<JsonDataResponse<List<Friend>>> GetFriends()
		{
			var getFriendsResponse = await _accountService.GetFriends(CurrentSessionOrFail);

			return getFriendsResponse.ToJsonResponse();
		}

		[ItemNotNull]
		[HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> AddFriend([NotNull][FromBody] AddFriendUiModel addFriendUiModel)
		{
			var message = new AddAsFriendModel()
			{
				SessionContext = CurrentSessionOrFail,
				AccountId = addFriendUiModel.AccountId,
			};

			var friendAddResponse = await _accountService.AddAsFriend(message);

			return friendAddResponse.ToJsonResponse();
		}
	}
}