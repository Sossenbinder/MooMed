using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using GreenPipes;
using JetBrains.Annotations;
using MassTransit;
using MassTransit.SignalR.Contracts;
using MassTransit.SignalR.Utils;
using Microsoft.AspNetCore.SignalR.Protocol;
using MooMed.Common.Definitions.Notifications;
using MooMed.Core.Code.Logging.Loggers.Interface;
using MooMed.Dns.Service.Interface;
using MooMed.Eventing.Events.MassTransit.Interface;
using MooMed.SignalR.Hubs;

namespace MooMed.Eventing.Events.MassTransit
{
	public class MassTransitEventingService : IStartable, IMassTransitEventingService
	{
		[NotNull]
		private readonly IMainLogger _logger;

		[NotNull]
		private readonly IBusControl _busControl;

		[NotNull]
		private readonly IDnsResolutionService _dnsResolutionService;

		[NotNull]
		private readonly List<IHubProtocol> _signalRProtocols;

		public MassTransitEventingService(
			[NotNull] IMainLogger logger,
			[NotNull] IDnsResolutionService dnsResolutionService,
			[NotNull] IBusControl busControl)
		{
			_logger = logger;
			_dnsResolutionService = dnsResolutionService;
			_busControl = busControl;

			_signalRProtocols = new List<IHubProtocol>(){ new JsonHubProtocol() };
		}

		public void Start()
		{
			var timeout = TimeSpan.FromSeconds(10);

			try
			{
				_busControl.Start(timeout);
				_logger.Info("Successfully initiated RabbitMQ EventBus.");
			}
			catch (Exception)
			{
				_logger.Info($"RabbitMQ EventBus didn't start within {timeout.TotalSeconds} seconds.");
			}
		}

		public async Task RaiseEvent<T>(T message)
			where T : class
		{
			await _busControl.Publish(message);
		}

		public Task RaiseSignalREvent<T>(FrontendNotification<T> notification)
		{
			var methodName = notification.NotificationType.ToString();

			var payload = new object[]
			{
				new
				{
					operation = notification.Operation,
					data = notification.Data,
				}
			};

			//TODO: Make this All/Group/User 
			return _busControl.Publish<All<NotificationHub>>(new
			{
				Messages = _signalRProtocols.ToProtocolDictionary(methodName, payload),
			});
		}

		private void RegisterForEvent<T>(string queueName, Func<ConsumeContext<T>, Task> handler)
			where T : class
		{
			// To ensure every queue is unique, add the DNS hostname to it
			queueName = $"{queueName}_{_dnsResolutionService.GetOwnHostName()}";

			_busControl.ConnectReceiveEndpoint(queueName, ep =>
			{
				ep.UseMessageRetry(retry => retry.Exponential(
					10, 
					TimeSpan.FromSeconds(2), 
					TimeSpan.FromMinutes(5), 
					TimeSpan.FromSeconds(10)));
				
				ep.Handler<T>(ctx => handler(ctx));
			});
		}

		public void RegisterForEvent<T>(string queueName, Func<T, Task> handler) where T : class
		{
			RegisterForEvent<T>(queueName, ctx => handler(ctx.Message));
		}
	}
}
