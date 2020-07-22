using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MooMed.Common.Definitions;

namespace MooMed.Frontend.Controllers.Base
{
    public class BaseController : Controller
    {
	    protected Language CurrentUiLanguage { get; private set; }

        public override Task OnActionExecutionAsync(
            [NotNull] ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            InitUserContext(context);

            return base.OnActionExecutionAsync(context, next);
        }

        private void InitUserContext([NotNull] ActionContext actionExecutingContext)
        {
            var uiLang = Language.en;
            
            if (actionExecutingContext.HttpContext.Request.Cookies["lang"] != null)
            {
                uiLang = (Language)Enum.Parse(typeof(Language), actionExecutingContext.HttpContext.Request.Cookies["lang"]);
            }

            CurrentUiLanguage = uiLang;
        }
    }
}