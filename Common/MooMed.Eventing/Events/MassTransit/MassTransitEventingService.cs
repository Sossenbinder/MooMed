using System;
using System.Threading.Tasks;
using GreenPipes;
using JetBrains.Annotations;
using MassTransit;
using MooMed.Core.Code.Helper.Retry;
using MooMed.Core.Code.Logging.Loggers.Interface;
using MooMed.Dns.Service.Interface;
using MooMed.Eventing.Events.MassTransit.Interface;

namespace MooMed.Eventing.Events.MassTransit
{
	public class MassTransitEventingService : IMassTransitEventingService
	{
		[NotNull]
		private readonly IMainLogger _logger;

		[NotNull]
		private readonly IBusControl _busControl;

		[NotNull]
		private readonly IDnsResolutionService _dnsResolutionService;

		public MassTransitEventingService(
			[NotNull] IMainLogger logger,
			[NotNull] IDnsResolutionService dnsResolutionService,
			[NotNull] IBusControl busControl)
		{
			_logger = logger;
			_dnsResolutionService = dnsResolutionService;
			_busControl = busControl;

			Task.Run(Start);
		}

		public async Task Start()
		{
			var timeBetween = TimeSpan.FromSeconds(10);

			await RetryStrategy.DoRetryExponential(() =>
			{
				_busControl.Start();

				_logger.Info("Successfully initiated RabbitMQ EventBus.");
			}, retryCount =>
			{
				_logger.Info($"Retrying rabbitMQ start for the {retryCount}# time");
				return Task.CompletedTask;
			}, timeBetween.Seconds);
		}

		public async Task RaiseEvent<T>(T message)
			where T : class
		{
			await _busControl.Publish(message);
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
		public void RegisterForEvent<T>(string queueName, Action<T> handler) where T : class
		{
			RegisterForEvent<T>(queueName, data =>
			{
				handler(data);
				return Task.CompletedTask;
			});
		}

		public void RegisterForEvent<T>(string queueName, Func<T, Task> handler) where T : class
		{
			RegisterForEvent<T>(queueName, ctx => handler(ctx.Message));
		}
	}
}
