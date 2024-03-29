﻿using System.Threading.Tasks;
using Grpc.Core;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.Definitions.Models.User.ErrorCodes;
using MooMed.Common.Definitions.UiModels.User;
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

		[NotNull]
		private readonly IAccountService _accountService;

		public ProfileController(
			[NotNull] ISessionService sessionService,
			[NotNull] IProfilePictureService profilePictureService,
			[NotNull] IAccountService accountService)
			: base(sessionService)
		{
			_profilePictureService = profilePictureService;
			_accountService = accountService;
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

		[Authorize]
		[HttpPost]
		public async Task<JsonDataResponse<IdentityErrorCode>> UpdatePersonalData([FromBody] PersonalDataUiModel personalData)
		{
			var account = CurrentSessionOrFail.Account;

			var hasChanges = (!personalData.Email?.Equals(account.Email) ?? false)
				|| (!personalData.UserName?.Equals(account.UserName) ?? false);

			if (!hasChanges)
			{
				return JsonDataResponse<IdentityErrorCode>.Success();
			}

			var response = await _accountService.UpdatePersonalData(new PersonalData()
			{
				UserName = personalData.UserName,
				Email = personalData.Email,
				SessionContext = CurrentSessionOrFail
			});

			return response.ToJsonResponse();
		}

		[Authorize]
		[HttpPost]
		public async Task<JsonDataResponse<IdentityErrorCode>> UpdatePassword([FromBody] UpdatePasswordUiModel passwordData)
		{
			var response = await _accountService.UpdatePassword(new UpdatePassword()
			{
				OldPassword = passwordData.OldPassword,
				NewPassword = passwordData.NewPassword,
				SessionContext = CurrentSessionOrFail
			});

			return response.ToJsonResponse();
		}
	}
}