using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.Filters;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.ServiceBase.Interface;

namespace MooMed.Web.Controllers.Base
{
    public class SessionBaseController : BaseController
    {
	    [NotNull]
	    private readonly ISessionService m_sessionService;

	    [CanBeNull]
	    protected ISessionContext CurrentSession { get; private set; }
		
	    [NotNull]
	    protected ISessionContext CurrentSessionOrFail
	    {
		    get
		    {
			    if (CurrentSession == null)
			    {
				    throw new NullReferenceException();
			    }

			    return CurrentSession;
		    }
	    }

	    [CanBeNull]
	    protected Account CurrentAccount => CurrentSession?.Account;

	    public SessionBaseController([NotNull] ISessionService sessionService)
	    {
		    m_sessionService = sessionService;
	    }

	    public override async Task OnActionExecutionAsync(
		    ActionExecutingContext context,
		    ActionExecutionDelegate next)
	    {
		    await InitUserContext(context);

		    await base.OnActionExecutionAsync(context, next);
	    }

	    private async Task InitUserContext([NotNull] ActionExecutingContext actionExecutingContext)
	    {
		    if (actionExecutingContext.HttpContext.IsAuthenticated())
		    {
			    var test = await m_sessionService.LoginAccount(new Account());
				var sessionServiceResponse = await m_sessionService.GetSessionContext(Convert.ToInt32(actionExecutingContext.HttpContext.User.Identity.Name));
				CurrentSession = sessionServiceResponse.PayloadOrNull;
		    }
	    }
	}
}