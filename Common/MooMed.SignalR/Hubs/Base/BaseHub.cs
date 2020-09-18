using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace MooMed.SignalR.Hubs.Base
{
	public abstract class BaseHub : Hub
	{
		public override async Task OnConnectedAsync()
		{
			await Task.WhenAll(
				Groups.AddToGroupAsync(Context.ConnectionId, Context.User.Identity.Name),
				base.OnConnectedAsync());
		}

		public override async Task OnDisconnectedAsync(Exception exception)
		{
			await Task.WhenAll(
				Groups.RemoveFromGroupAsync(Context.ConnectionId, Context.User.Identity.Name),
				base.OnDisconnectedAsync(exception));
		}
	}
}