using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MooMed.Common.Definitions.UiModels.User;
using MooMed.Frontend.Controllers.Base;
using MooMed.Frontend.Controllers.Result;
using MooMed.Grpc.Services.Interface;

namespace MooMed.Frontend.Controllers
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
		public async Task<JsonResponse> GetAccount([NotNull] [FromBody] GetAccountUiModel getAccountUiModel)
		{
			var accountResponse = await _accountService.FindById(getAccountUiModel.AccountId);
			return accountResponse.ToJsonResponse();
		}

		[NotNull]
		[HttpGet]
		[ItemNotNull]
		[Authorize]
		public async Task<JsonResponse> GetProfilePicturePath()
		{
			var profilePicturePath = await _profilePictureService.GetProfilePictureForAccount(CurrentSession);

			return profilePicturePath.ToJsonResponse();
		}
    }
}