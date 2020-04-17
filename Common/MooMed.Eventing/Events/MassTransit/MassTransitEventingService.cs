using System;
using System.Threading.Tasks;
using Autofac;
using GreenPipes;
using JetBrains.Annotations;
using MassTransit;
using MassTransit.SignalR.Contracts;
using MassTransit.SignalR.Utils;
using Microsoft.AspNetCore.SignalR.Protocol;
using MooMed.Core.Code.Logging.Loggers.Interface;
using MooMed.Dns.Service.Interface;
using MooMed.Eventing.Events.MassTransit.Interface;
using MooMed.Eventing.Helper;
using MooMed.SignalR.Hubs;

namespace MooMed.Eventing.Events.MassTransit
{
	public class MassTransitEventingService : IStartable, IMassTransitEventingService
	{
		[NotNull]
		private readonly IMainLogger _logger;

		[NotNull]
		private readonly IBusControl _eventBus;

		[NotNull]
		private readonly IDnsResolutionService _dnsResolutionService;

		public MassTransitEventingService(
			[NotNull] IMainLogger logger,
			[NotNull] IDnsResolutionService dnsResolutionService)
		{
			_logger = logger;
			_dnsResolutionService = dnsResolutionService;

			_eventBus = MassTransitBusFactory.CreateBus(dnsResolutionService, logger);
		}

		public void Start()
		{
			var timeout = TimeSpan.FromSeconds(10);

			try
			{
				_eventBus.Start(timeout);
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
			await _eventBus.Publish(message);
		}

		public Task RaiseSignalREvent<T>(T message)
		{
			var protocols = new IHubProtocol[] { new JsonHubProtocol() };

			return _eventBus.Publish<All<NotificationHub>>(new
			{
				Messages = protocols.ToProtocolDictionary("Test", new object []{"blub"}),
			});
		}

		private void RegisterForEvent<T>(string queueName, Func<ConsumeContext<T>, Task> handler)
			where T : class
		{
			// To ensure every queue is unique, add the DNS hostname to it
			queueName = $"{queueName}_{_dnsResolutionService.GetOwnHostName()}";

			_eventBus.ConnectReceiveEndpoint(queueName, ep =>
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
