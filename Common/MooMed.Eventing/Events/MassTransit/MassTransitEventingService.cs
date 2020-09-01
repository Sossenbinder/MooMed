using System;
using System.Threading.Tasks;
using GreenPipes;
using JetBrains.Annotations;
using MassTransit;
using MooMed.Common.Definitions.Logging;
using MooMed.Core.Code.Helper.Retry;
using MooMed.Eventing.Events.MassTransit.Interface;
using MooMed.Identity.Service.Identity.Interface;

namespace MooMed.Eventing.Events.MassTransit
{
	public class MassTransitEventingService : IMassTransitEventingService
	{
		[NotNull]
		private readonly IMooMedLogger _mooMedLogger;

		[NotNull]
		private readonly IBusControl _busControl;

		[NotNull]
		private readonly IServiceIdentityProvider _serviceIdentityProvider;

		public MassTransitEventingService(
			[NotNull] IMooMedLogger mooMedLogger,
			[NotNull] IServiceIdentityProvider serviceIdentityProvider,
			[NotNull] IBusControl busControl)
		{
			_mooMedLogger = mooMedLogger;
			_serviceIdentityProvider = serviceIdentityProvider;
			_busControl = busControl;

			Task.Run(Start);
		}

		public async Task Start()
		{
			var timeBetween = TimeSpan.FromSeconds(10);

			await RetryStrategy.DoRetryExponential(() =>
			{
				_busControl.Start();

				_mooMedLogger.Info("Successfully initiated RabbitMQ EventBus.");
			}, retryCount =>
			{
				_mooMedLogger.Info($"Retrying rabbitMQ start for the {retryCount}# time");
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
			// To ensure every queue is unique, add the DNS hostname to it. Every consumer needs a unique
			// id
			queueName = $"{queueName}_{_serviceIdentityProvider.GetServiceIdentity()}";

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