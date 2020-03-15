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
	    private readonly IAccountService m_accountService;

		[NotNull]
		private readonly IProfilePictureService m_profilePictureService;

		public AccountController(
	        [NotNull] ISessionService sessionService,
	        [NotNull] IAccountService accountService,
			[NotNull] IProfilePictureService profilePictureService) 
            : base(sessionService)
		{
			m_accountService = accountService;
			m_profilePictureService = profilePictureService;
		}

        [NotNull]
        [Authorize]
		[HttpGet]
		public JsonResponse GetOwnAccount()
        {
            return JsonResponse.Success(CurrentAccount);
        }

		[NotNull]
		[Authorize]
		[HttpGet]
		public async Task<JsonResponse> GetAccount(int accountId)
		{
			var account = await m_accountService.FindById(accountId);
			return JsonResponse.Success(account);
		}

		[NotNull]
		[HttpGet]
		[ItemNotNull]
		public async Task<JsonResponse> GetProfilePicturePath()
		{
			var profilePicturePath = await m_profilePictureService.GetProfilePictureForAccount(CurrentSession);

			return profilePicturePath.ToJsonResponse();
		}
    }
}