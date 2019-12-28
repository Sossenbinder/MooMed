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
		private readonly IProfilePictureService m_profilePictureService;

		public AccountController(
	        [NotNull] ISessionService sessionService,
	        [NotNull] IProfilePictureService profilePictureService) 
            : base(sessionService)
		{
			m_profilePictureService = profilePictureService;
		}

        [NotNull]
        [Authorize]
		[HttpGet]
		public JsonResponse GetAccount()
        {
            return JsonResponse.Success(CurrentAccount);
        }

		[NotNull]
		[HttpGet]
		[ItemNotNull]
		public async Task<JsonResponse> GetProfilePicturePath()
		{
			var profilePicturePath = await m_profilePictureService.GetProfilePictureForAccount(CurrentSession);

			return JsonResponse.Success(profilePicturePath);
		}
    }
}