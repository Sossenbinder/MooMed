using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.Filters;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;
using MooMed.Logging.Loggers;
using MooMed.ServiceBase.Services.Interface;

namespace MooMed.Frontend.Controllers.Base
{
	public class SessionBaseController : BaseController
	{
		private readonly ISessionService _sessionService;

		protected ISessionContext? CurrentSession { get; private set; }

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

		protected Account? CurrentAccountOrNull => CurrentSession?.Account;

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
					StaticLogger.Fatal("Request was authenticated, but couldn't retrieve SessionContext for account", accountId);
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