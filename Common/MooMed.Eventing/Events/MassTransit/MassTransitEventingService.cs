using System;
using System.Threading.Tasks;
using Autofac;
using JetBrains.Annotations;
using MassTransit;
using MooMed.Common.Definitions.IPC;
using MooMed.Core.Code.Helper.Retry;
using MooMed.Core.Code.Logging.Loggers.Interface;
using MooMed.Dns.Service.Interface;
using MooMed.Eventing.Events.MassTransit.Interface;

namespace MooMed.Eventing.Events.MassTransit
{
	public class MassTransitEventingService : IStartable, IMassTransitEventingService
	{
		[NotNull]
		private readonly IMainLogger m_logger;

		[NotNull]
		private readonly IBusControl m_eventBus;

		public MassTransitEventingService(
			[NotNull] IMainLogger logger,
			[NotNull] IDnsResolutionService dnsResolutionService)
		{
			m_logger = logger;

			m_eventBus = Bus.Factory.CreateUsingRabbitMq(async config =>
			{
				await RetryStrategy.DoRetry(async () =>
				{
					var deploymentIp = await dnsResolutionService.ResolveDeploymentToIp(Deployment.RabbitMq);
					config.Host($"rabbitmq://{deploymentIp}");
				}, retryCount =>
				{
					logger.Info($"Retrying RabbitMQ setup for the {retryCount}# time");
					return Task.CompletedTask;
				});
			});
		}

		public void Start()
		{
			var timeout = TimeSpan.FromSeconds(10);

			try
			{
				m_eventBus.Start(timeout);
				m_logger.Info("Successfully initiated RabbitMQ EventBus.");
			}
			catch (Exception)
			{
				m_logger.Info($"RabbitMQ EventBus didn't start within {timeout.TotalSeconds} seconds.");
			}
		}

		public Task RaiseEvent<T>(T message)
			where T : class
		{
			return m_eventBus.Publish(message);
		}

		public void RegisterForEvent<T>(string queueName, Func<ConsumeContext<T>, Task> handler)
			where T : class
		{
			m_eventBus.ConnectReceiveEndpoint(queueName, ep =>
			{
				ep.Handler<T>(ctx => handler(ctx));
			});
		}

		public void RegisterForEvent<T>(string queueName, Action<T> handler) where T : class
		{
			RegisterForEvent<T>(queueName, ctx =>
			{
				handler(ctx.Message);

				return Task.CompletedTask;
			});
		}

		public void RegisterForEvent<T>(string queueName, Func<T, Task> handler) where T : class
		{
			RegisterForEvent<T>(queueName, ctx => handler(ctx.Message));
		}
	}
}
