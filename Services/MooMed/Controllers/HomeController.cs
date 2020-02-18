using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Web.Controllers.Base;
using MooMed.Web.Models;

namespace MooMed.Web.Controllers
{
    public class HomeController : SessionBaseController
    {
	    [NotNull]
	    private readonly IAccountService m_accountService;

	    public HomeController(
	        [NotNull] ISessionService sessionService,
	        [NotNull] IAccountService accountService) 
            : base(sessionService)
	    {
		    m_accountService = accountService;
	    }

        public async Task<ActionResult> Index([CanBeNull] string route)
        {
            if (!Request.HttpContext.User.Identity.IsAuthenticated)
            {
                return View("~/Views/Logon/Landing.cshtml", new ControllerMetaData("MooMed - Logon", CurrentUiLanguage, route, null));
            }

            if (CurrentSession == null)
            {
                await m_accountService.RefreshLoginForAccount(Convert.ToInt32(User.Identity.Name));
            }

            return View(new ControllerMetaData("MooMed", CurrentUiLanguage, route, null));
        }
        
        /// <summary>
        /// Route which is hit if a request comes in which is NOT known by the RouteConfig.cs, as in possibly every react-router route
        /// </summary>
        /// <returns></returns>
        public Task<ActionResult> SpaFallback()
        {
	        var routeToForward = HttpContext.GetRouteData().Values["Path"] as string ?? "";

	        return Index(routeToForward);
        }
    }
}