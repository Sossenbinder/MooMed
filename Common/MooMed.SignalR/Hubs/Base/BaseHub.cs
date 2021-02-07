using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace MooMed.SignalR.Hubs.Base
{
	public abstract class BaseHub : Hub
	{
		public override Task OnConnectedAsync()
		{
			return Task.WhenAll(
				Groups.AddToGroupAsync(Context.ConnectionId, Context.User.Identity.Name),
				base.OnConnectedAsync());
		}

		public override Task OnDisconnectedAsync(Exception exception)
		{
			return Task.WhenAll(
				Groups.RemoveFromGroupAsync(Context.ConnectionId, Context.User.Identity.Name),
				base.OnDisconnectedAsync(exception));
		}
	}
}