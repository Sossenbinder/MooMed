using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MassTransit;
using MassTransit.SignalR.Contracts;
using MassTransit.SignalR.Utils;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Protocol;
using MooMed.Common.Definitions.Notifications;
using MooMed.Eventing.Events.MassTransit.Interface;
using MooMed.SignalR.Hubs;

namespace MooMed.Eventing.Events.MassTransit
{
	public class MassTransitSignalRBackplaneService : IMassTransitSignalRBackplaneService
	{
		[NotNull]
		private readonly IBusControl _busControl;

		[NotNull]
		private readonly List<IHubProtocol> _signalRProtocols;

		public MassTransitSignalRBackplaneService([NotNull] IBusControl busControl)
		{
			_busControl = busControl;

			_signalRProtocols = new List<IHubProtocol>() { new JsonHubProtocol() };
		}

		public async Task RaiseAllSignalREvent<T>(FrontendNotification<T> notification, string[] excludedConnectionIds = null)
		{
			await _busControl.Publish<All<SignalRHub>>(new
			{
				Messages = _signalRProtocols.ToProtocolDictionary("Test", new object[] {"blub"}),
			});

			var signalRParams = new
			{
				ExcludedConnectionIds = excludedConnectionIds,
				Messages = _signalRProtocols.ToProtocolDictionary(
					notification.NotificationType.ToString(),
					GetPayload(notification)),
			};

			await _busControl.Publish<All<SignalRHub>>(signalRParams);
		}

		public async Task RaiseConnectionSignalREvent<T>(string connectionId, FrontendNotification<T> notification)
		{
			var signalRParams = new
			{
				ConnectionId = connectionId,
				Messages = _signalRProtocols.ToProtocolDictionary(
					notification.NotificationType.ToString(),
					GetPayload(notification)),
			};

			await _busControl.Publish<Connection<SignalRHub>>(signalRParams);
		}

		public async Task RaiseGroupSignalREvent<T>(string groupName, FrontendNotification<T> notification, string[] excludedConnectionIds = null)
		{
			var signalRParams = new
			{
				GroupName = groupName,
				ExcludedConnectionIds = excludedConnectionIds,
				Messages = _signalRProtocols.ToProtocolDictionary(
					notification.NotificationType.ToString(),
					GetPayload(notification)),
			};

			await _busControl.Publish<Group<SignalRHub>>(signalRParams);
		}

		public async Task RaiseUserSignalREvent<T>(string userId, FrontendNotification<T> notification)
		{
			var signalRParams = new
			{
				UserId = userId,
				Messages = _signalRProtocols.ToProtocolDictionary(
					notification.NotificationType.ToString(),
					GetPayload(notification)),
			};

			await _busControl.Publish<User<SignalRHub>>(signalRParams);
		}

		private object[] GetPayload<T>([NotNull] FrontendNotification<T> notification)
		{
			var payload = new object[]
			{
				new
				{
					operation = notification.Operation,
					data = notification.Data,
				}
			};

			return payload;
		}
	}
}
