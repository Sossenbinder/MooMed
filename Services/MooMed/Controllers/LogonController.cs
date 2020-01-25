using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Core.DataTypes;
using MooMed.Web.Controllers.Base;
using MooMed.Web.Controllers.Result;
using ProtoBuf.Meta;
using Serilog;

namespace MooMed.Web.Controllers
{
    [Authorize]
    public class LogonController : SessionBaseController
	{
		[NotNull]
		private readonly IAccountService m_accountService;

		public LogonController(
	        [NotNull] ISessionService sessionService,
	        [NotNull] IAccountService accountService) 
            : base(sessionService)
		{
			m_accountService = accountService;
		}

        //
        // POST: /Logon/Login
        [ItemNotNull]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login([NotNull] LoginModel loginModel)
        {
	        var types = RuntimeTypeModel.Default.GetTypes();

            var serviceResponse = await m_accountService.Login(loginModel);
			
            if (serviceResponse.IsSuccess)
            {
	            await HttpContext.SignInAsync(
		            CookieAuthenticationDefaults.AuthenticationScheme,
		            new ClaimsPrincipal(
			            new ClaimsIdentity(
				            new List<Claim>
				            {
					            new Claim(type: ClaimTypes.Name, value: serviceResponse.PayloadOrFail.Account.Id.ToString())
				            }
				            , "login"
			            )
		            ),
		            new AuthenticationProperties()
		            {
			            IsPersistent = loginModel.RememberMe
		            }
	            );
            }

            return serviceResponse.ToJsonResponse();
        }

        [ItemNotNull]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register([NotNull] RegisterModel registerModel)
        {
            var result = await m_accountService.Register(registerModel, CurrentUiLanguage);

            return result.IsSuccess ? JsonResponse.Success() : JsonResponse.Error(result.RegistrationValidationResult);
        }

        [ItemNotNull]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> LogOff()
        {
            await m_accountService.LogOff(CurrentSessionOrFail);

            await HttpContext.SignOutAsync();

            return JsonResponse.Success();
        }
    }
}