using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Web.Hubs.Base;
using MooMed.Web.Hubs.Interface;

namespace MooMed.Web.Hubs
{
	public class NotificationHub : BaseHub, INotificationHub
	{
		public NotificationHub([NotNull] ISessionService sessionService) 
			: base(sessionService)
		{
		}

		public async Task NewMessage(string username, string message)
		{
			await Clients.All.SendAsync("NewNotification", "bla");
		}
	}
}
