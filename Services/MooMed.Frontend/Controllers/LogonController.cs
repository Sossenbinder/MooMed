using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.Definitions.UiModels.User;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Frontend.Controllers.Base;
using MooMed.Frontend.Controllers.Result;

namespace MooMed.Frontend.Controllers
{
    [Authorize]
    public class LogonController : SessionBaseController
	{
		[NotNull]
		private readonly IAccountService _accountService;

		public LogonController(
	        [NotNull] ISessionService sessionService,
	        [NotNull] IAccountService accountService) 
            : base(sessionService)
		{
			_accountService = accountService;
		}

        // POST: /Logon/Login
        [ItemNotNull]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login([NotNull] [FromBody] LoginModel loginModel)
		{
			var serviceResponse = await _accountService.Login(loginModel);

			if (!serviceResponse.IsSuccess)
			{
				return serviceResponse.ToJsonResponse();
			}

			var account = serviceResponse.PayloadOrFail.Account;

	        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
	        identity.AddClaim(new Claim(ClaimTypes.Name, account.Id.ToString()));

	        var principal = new ClaimsPrincipal(identity);

	        await HttpContext.SignInAsync(
		        CookieAuthenticationDefaults.AuthenticationScheme,
		        principal,
		        new AuthenticationProperties()
		        {
			        IsPersistent = loginModel.RememberMe
		        }
	        );

	        return serviceResponse.ToJsonResponse();
        }

        [ItemNotNull]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register([NotNull] [FromBody] RegisterUiModel registerModel)
        {
	        var model = registerModel.ToModel();
	        model.Language = CurrentUiLanguage;

            var result = await _accountService.Register(model);

            return result.IsSuccess ? JsonResponse.Success() : JsonResponse.Error(result.PayloadOrNull);
        }

        [ItemNotNull]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> LogOff()
        {
            await _accountService.LogOff(CurrentSessionOrFail);

            await HttpContext.SignOutAsync();

            return JsonResponse.Success();
        }
    }
}