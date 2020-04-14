using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using MooMed.Common.ServiceBase.Interface;

namespace MooMed.Web.Hubs.Base
{
	public class BaseHub : Hub
	{
		[NotNull]
		private readonly ISessionService _sessionService;

		protected BaseHub([NotNull] ISessionService sessionService)
		{
			_sessionService = sessionService;
		}

		public override async Task OnConnectedAsync()
		{
			var accountId = Convert.ToInt32(Context.User.Identity.Name);
			var sessionServiceResponse = await _sessionService.GetSessionContext(accountId);

			await base.OnConnectedAsync();
		}

		public override Task OnDisconnectedAsync(Exception exception)
		{
			return base.OnDisconnectedAsync(exception);
		}
	}
}
