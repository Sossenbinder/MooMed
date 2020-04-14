using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Web.Controllers.Base;
using MooMed.Web.Controllers.Result;

namespace MooMed.Web.Controllers
{
	[Authorize]
    public class AccountController : SessionBaseController
    {
	    [NotNull]
	    private readonly IAccountService _accountService;

		[NotNull]
		private readonly IProfilePictureService _profilePictureService;

		public AccountController(
	        [NotNull] ISessionService sessionService,
	        [NotNull] IAccountService accountService,
			[NotNull] IProfilePictureService profilePictureService) 
            : base(sessionService)
		{ 
			_accountService = accountService;
			_profilePictureService = profilePictureService;
		}

        [NotNull]
        [Authorize]
		[HttpGet]
		public JsonResponse GetOwnAccount()
        {
            return JsonResponse.Success(CurrentAccountOrNull);
        }

		[NotNull]
		[Authorize]
		[HttpPost]
		public async Task<JsonResponse> GetAccount(int accountId)
		{
			var accountResponse = await _accountService.FindById(accountId);
			return accountResponse.ToJsonResponse();
		}

		[NotNull]
		[HttpGet]
		[ItemNotNull]
		public async Task<JsonResponse> GetProfilePicturePath()
		{
			var profilePicturePath = await _profilePictureService.GetProfilePictureForAccount(CurrentSession);

			return profilePicturePath.ToJsonResponse();
		}
    }
}