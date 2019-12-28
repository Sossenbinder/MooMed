using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Core.Code.Extensions;
using MooMed.Core.DataTypes;
using MooMed.Web.Controllers.Base;
using MooMed.Web.Controllers.Result;

namespace MooMed.Web.Controllers
{
    public class ProfileController : SessionBaseController
	{
		[NotNull]
		private readonly IProfilePictureService m_profilePictureService;

		public ProfileController(
	        [NotNull] ISessionService sessionService,
	        [NotNull] IProfilePictureService profilePictureService) 
            : base(sessionService)
		{
			m_profilePictureService = profilePictureService;
		}

        [ItemNotNull]
        [Authorize]
        public async Task<ActionResult> UploadProfilePicture([NotNull] IFormCollection form)
        {
            if (form.Files?[0] == null || form.Files[0].Length == 0)
            {
                return JsonResponse.Error();
            }

            var formFile = form.Files[0];

            var profilePictureData = new ProfilePictureData();

            bool uploadResult;
            using (var imgStream = formFile.OpenReadStream())
            {
	            profilePictureData.RawData = imgStream.ReadToEnd();
				profilePictureData.FileExtension = formFile.GetFileExtension();

				uploadResult = false;
				//uploadResult = await m_profilePictureService.ProcessUploadedProfilePicture(CurrentSessionOrFail, profilePictureData);
            }

            if (uploadResult)
            {
                var newProfilePicturePath = await m_profilePictureService.GetProfilePictureForAccount(CurrentSession);

                return JsonResponse.Success(newProfilePicturePath);                  
            }

            return JsonResponse.Error();
        }
    }
}