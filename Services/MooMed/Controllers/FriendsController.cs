using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MooMed.Common.Definitions.Messages.Account;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Web.Controllers.Base;
using MooMed.Web.Controllers.Result;

namespace MooMed.Web.Controllers
{
	public class FriendsController : SessionBaseController
	{
		[NotNull]
		private readonly IAccountService m_accountService;

		public FriendsController(
			[NotNull] ISessionService sessionService,
			[NotNull] IAccountService accountService) 
			: base(sessionService)
		{
			m_accountService = accountService;
		}

		[ItemNotNull]
		[HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
		public Task<ActionResult> GetFriends()
		{
			return Task.FromResult(null as ActionResult);
		}

		[ItemNotNull]
		[HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> AddFriend(int accountId)
		{
			var message = new AddAsFriendMessage()
			{
				SessionContext = CurrentSession,
				AccountId = accountId,
			};

			var friendAddResponse = await m_accountService.AddAsFriend(message);

			return friendAddResponse.ToJsonResponse();
		}
	}
}
