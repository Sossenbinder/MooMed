using System.Threading.Tasks;
using Grpc.Core;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Core.Code.Extensions;
using MooMed.Web.Controllers.Base;
using MooMed.Web.Controllers.Result;
using ProtoBuf.Grpc;

namespace MooMed.Web.Controllers
{
    public class ProfileController : SessionBaseController
	{
		[NotNull]
		private readonly IProfilePictureService m_profilePictureService;

		[NotNull]
		private readonly ISessionService m_sessionService;

		public ProfileController(
	        [NotNull] ISessionService sessionService,
	        [NotNull] IProfilePictureService profilePictureService) 
            : base(sessionService)
		{
			m_profilePictureService = profilePictureService;
			m_sessionService = sessionService;
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

            bool uploadResult;

            await using (var imgStream = formFile.OpenReadStream())
            {
	            var callOptions = new CallOptions(new Metadata()
	            {
		            {"fileextension", formFile.GetFileExtension()},
		            {"accountid", CurrentAccountOrFail.Id.ToString()},
	            });
				var callContext = new CallContext(callOptions);

                uploadResult = (await m_profilePictureService.ProcessUploadedProfilePicture(imgStream.ReadAsAsyncEnumerable(), callContext)).PayloadOrNull;
            }

            if (uploadResult)
            {
                var newProfilePicturePath = await m_profilePictureService.GetProfilePictureForAccount(CurrentSession);

                CurrentAccountOrFail.ProfilePicturePath = newProfilePicturePath.PayloadOrNull;
                await m_sessionService.UpdateSessionContext(CurrentSessionOrFail);

                return JsonResponse.Success(newProfilePicturePath);                  
            }

            return JsonResponse.Error();
        }
    }
}