﻿using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using MooMed.Frontend.Controllers.Base;
using MooMed.Frontend.Models;
using MooMed.ServiceBase.Services.Interface;

namespace MooMed.Frontend.Controllers
{
	public class HomeController : SessionBaseController
	{
		[NotNull]
		private readonly IAccountService _accountService;

		public HomeController(
			[NotNull] ISessionService sessionService,
			[NotNull] IAccountService accountService)
			: base(sessionService)
		{
			_accountService = accountService;
		}

		public async Task<ActionResult> Index()
		{
			if (!Request.HttpContext.User.Identity.IsAuthenticated)
			{
				return View("~/Views/Logon/Landing.cshtml", new ControllerMetaData("MooMed - Logon", CurrentUiLanguage, null));
			}

			if (CurrentSession == null)
			{
				await _accountService.RefreshLoginForAccount(Convert.ToInt32(User.Identity.Name));
			}

			return View(new ControllerMetaData("MooMed", CurrentUiLanguage, null));
		}
	}
}