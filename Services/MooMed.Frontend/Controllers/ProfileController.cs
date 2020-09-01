using System.Threading.Tasks;
using Grpc.Core;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MooMed.DotNet.Extensions;
using MooMed.Frontend.Controllers.Base;
using MooMed.Frontend.Controllers.Result;
using MooMed.ServiceBase.Services.Interface;
using ProtoBuf.Grpc;

namespace MooMed.Frontend.Controllers
{
	public class ProfileController : SessionBaseController
	{
		[NotNull]
		private readonly IProfilePictureService _profilePictureService;

		[NotNull]
		private readonly ISessionService _sessionService;

		public ProfileController(
			[NotNull] ISessionService sessionService,
			[NotNull] IProfilePictureService profilePictureService)
			: base(sessionService)
		{
			_profilePictureService = profilePictureService;
			_sessionService = sessionService;
		}

		[ItemNotNull]
		[Authorize]
		[HttpPost]
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

				var uploadResponse = await _profilePictureService.ProcessUploadedProfilePicture(imgStream.ReadAsAsyncEnumerable(), callContext);
				uploadResult = uploadResponse.PayloadOrNull;
			}

			if (!uploadResult)
			{
				return JsonResponse.Error();
			}

			var newProfilePicturePath = await _profilePictureService.GetProfilePictureForAccount(CurrentSession);

			CurrentAccountOrFail.ProfilePicturePath = newProfilePicturePath.PayloadOrNull;
			await _sessionService.UpdateSessionContext(CurrentSessionOrFail);

			return JsonResponse.Success(newProfilePicturePath);
		}
	}
}