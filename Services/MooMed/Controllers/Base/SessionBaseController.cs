using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.Filters;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Core.Code.Logging.Loggers;

namespace MooMed.Web.Controllers.Base
{
    public class SessionBaseController : BaseController
    {
	    [NotNull]
	    private readonly ISessionService _sessionService;

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
	    protected Account CurrentAccountOrNull => CurrentSession?.Account;

	    [NotNull]
	    protected Account CurrentAccountOrFail => CurrentSession?.Account ?? throw new InvalidOperationException();

		public SessionBaseController([NotNull] ISessionService sessionService)
	    {
		    _sessionService = sessionService;
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
			    var accountId = Convert.ToInt32(actionExecutingContext.HttpContext.User.Identity.Name);
				var sessionServiceResponse = await _sessionService.GetSessionContext(accountId);

				if (sessionServiceResponse.IsFailure)
				{
					StaticLogger.Fatal("Couldn't retrieve SessionContext.", accountId);
				}
				else
				{
					StaticLogger.Info("Retrieved SessionContext..", accountId);
					CurrentSession = sessionServiceResponse.PayloadOrFail;
				}
		    }
	    }
	}
}