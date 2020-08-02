﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace MooMed.SignalR.Hubs.Base
{
	public abstract class BaseHub : Hub
	{
		public override async Task OnConnectedAsync()
		{
			await Groups.AddToGroupAsync(Context.ConnectionId, Context.User.Identity.Name);
			await base.OnConnectedAsync();
		}

		public override async Task OnDisconnectedAsync(Exception exception)
		{
			await Groups.RemoveFromGroupAsync(Context.ConnectionId, Context.User.Identity.Name);
			await base.OnDisconnectedAsync(exception);
		}
	}
}