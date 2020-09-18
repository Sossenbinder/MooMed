using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MooMed.Common.Definitions.Models.User.ErrorCodes;
using MooMed.Common.Definitions.UiModels.User;
using MooMed.Frontend.Controllers.Base;
using MooMed.Frontend.Controllers.Result;
using MooMed.Frontend.Models;
using MooMed.Logging.Loggers;
using MooMed.Module.AccountValidation.Converters;
using MooMed.ServiceBase.Services.Interface;

namespace MooMed.Frontend.Controllers
{
	public class AccountValidationController : BaseController
	{
		[NotNull]
		private readonly IAccountValidationService _accountValidationService;

		[NotNull]
		private readonly AccountValidationUiModelConverter _accountValidationUiModelConverter;

		public AccountValidationController(
			[NotNull] IAccountValidationService accountValidationService,
			[NotNull] AccountValidationUiModelConverter accountValidationUiModelConverter)
		{
			_accountValidationService = accountValidationService;
			_accountValidationUiModelConverter = accountValidationUiModelConverter;
		}

		/// <summary>
		/// Return the base validation page where user is provided some information
		/// </summary>
		/// <param name="token"></param>
		/// <returns></returns>
		public ViewResult Index()
		{
			return View("~/Views/Other/Other.cshtml", new ControllerMetaData("MooMed - AccountValidation", CurrentUiLanguage));
		}

		/// <summary>
		/// Validate registration of an account
		/// </summary>
		/// <param name="accountValidationUiModel"></param>
		/// <returns></returns>
		[ItemNotNull]
		[HttpPost]
		[AllowAnonymous]
		public async Task<JsonDataResponse<IdentityErrorCode>> ValidateRegistration([NotNull][FromBody] AccountValidationUiModel accountValidationUiModel)
		{
			var model = _accountValidationUiModelConverter.ToModel(accountValidationUiModel);

			var result = await _accountValidationService.ValidateRegistration(model);

			return result.ToJsonResponse();
		}
	}
}