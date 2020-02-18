using System;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using MooMed.Common.Definitions;
using MooMed.Core.Translations;
using MooMed.Web.Controllers.Base;
using MooMed.Web.Models;

namespace MooMed.Web.Controllers
{
    public class OtherController : BaseController
    {
        // GET: Other
        public ActionResult Index([CanBeNull] string route)
        {
            if (route != null)
            {
                ViewBag.reactRoute = route;
            }

            ViewBag.lang = Enum.GetName(typeof(Language), CurrentUiLanguage);

            return View("~/Views/Other/Other.cshtml", new ControllerMetaData("Other", CurrentUiLanguage, route, null));
        }
    }
}