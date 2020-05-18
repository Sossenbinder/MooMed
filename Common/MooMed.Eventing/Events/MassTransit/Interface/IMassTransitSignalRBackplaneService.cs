﻿using System.Threading.Tasks;
using MooMed.Common.Definitions.Notifications;

namespace MooMed.Eventing.Events.MassTransit.Interface
{
	public interface IMassTransitSignalRBackplaneService
	{
		Task RaiseAllSignalREvent<T>(FrontendNotification<T> notification, string[] excludedConnectionIds = null);

		Task RaiseConnectionSignalREvent<T>(string connectionId, FrontendNotification<T> notification);

		Task RaiseGroupSignalREvent<T>(string groupName, FrontendNotification<T> notification, string[] excludedConnectionIds = null);

		Task RaiseUserSignalREvent<T>(string userId, FrontendNotification<T> notification);
	}
}