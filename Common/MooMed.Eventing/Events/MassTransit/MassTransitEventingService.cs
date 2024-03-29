﻿using System;
using System.Threading.Tasks;
using GreenPipes;
using JetBrains.Annotations;
using MassTransit;
using MooMed.Core.Code.Helper.Retry;
using MooMed.DotNet.Extensions;
using MooMed.Eventing.DataTypes;
using MooMed.Eventing.Events.MassTransit.Interface;
using MooMed.Identity.Service.Identity.Interface;
using MooMed.Logging.Abstractions.Interface;

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

			_ = Start();
		}

		public async Task Start()
		{
			var timeBetween = TimeSpan.FromSeconds(5);

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

		public Task RaiseEvent<T>(T message)
			where T : class
		{
			return _busControl.Publish(message);
		}

		private void RegisterForEvent<T>(string queueName, Func<ConsumeContext<T>, Task> handler, QueueType queueType = QueueType.RegularQueue)
			where T : class
		{
			RegisterOnQueue(queueName, ep => ep.Handler<T>(ctx => handler(ctx)), queueType);
		}

		public void RegisterForEvent<T>(string queueName, Action<T> handler, QueueType queueType = QueueType.RegularQueue)
			where T : class
		{
			RegisterForEvent<T>(queueName, handler.MakeTaskCompatible()!, queueType);
		}

		public void RegisterForEvent<T>(string queueName, Func<T, Task> handler, QueueType queueType = QueueType.RegularQueue) where T : class
		{
			RegisterForEvent<T>(queueName, ctx => handler(ctx.Message), queueType);
		}

		public void RegisterConsumer<TConsumer>(string queueName, TConsumer consumer, QueueType queueType = QueueType.RegularQueue)
			where TConsumer : class, IConsumer
		{
			RegisterOnQueue(queueName, ep => ep.Instance(consumer), queueType);
		}

		private void RegisterOnQueue(string queueName, Action<IReceiveEndpointConfigurator> registrationCb, QueueType queueType = QueueType.RegularQueue)
		{
			queueName = $"{queueName}_{_serviceIdentityProvider.GetServiceIdentity()}";

			if (queueType == QueueType.ErrorQueue)
			{
				queueName = $"{queueName}_error";
			}

			_busControl.ConnectReceiveEndpoint(queueName, ep =>
			{
				ep.UseMessageRetry(retry => retry.Exponential(
					10,
					TimeSpan.FromSeconds(2),
					TimeSpan.FromMinutes(5),
					TimeSpan.FromSeconds(10)));

				registrationCb(ep);
			});
		}
	}
}