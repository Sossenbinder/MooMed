using System;
using Microsoft.AspNetCore.Mvc;
using MooMed.Common.Definitions;
using MooMed.Frontend.Controllers.Base;
using MooMed.Frontend.Models;

namespace MooMed.Frontend.Controllers
{
    public class OtherController : BaseController
    {
        // GET: Other
        public ActionResult Index()
        {
            ViewBag.lang = Enum.GetName(typeof(Language), CurrentUiLanguage);

            return View("~/Views/Other/Other.cshtml", new ControllerMetaData("Other", CurrentUiLanguage, null));
        }
    }
}