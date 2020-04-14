using System;
using System.Threading.Tasks;
using Autofac;
using GreenPipes;
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

		[NotNull]
		private readonly IDnsResolutionService m_dnsResolutionService;

		public MassTransitEventingService(
			[NotNull] IMainLogger logger,
			[NotNull] IDnsResolutionService dnsResolutionService)
		{
			m_logger = logger;
			m_dnsResolutionService = dnsResolutionService;

			m_eventBus = Bus.Factory.CreateUsingRabbitMq(async config =>
			{
				config.PurgeOnStartup = true;

				await RetryStrategy.DoRetryExponential(async () =>
				{
					var deploymentIp = await dnsResolutionService.ResolveDeploymentToIp(Deployment.RabbitMqManagement);

					if (deploymentIp == null)
					{
						throw new NullReferenceException("Couldn't resolve IP");
					}
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

		private void RegisterForEvent<T>(string queueName, Func<ConsumeContext<T>, Task> handler)
			where T : class
		{
			// To ensure every queue is unique, add the DNS hostname to it
			queueName = $"{queueName}_{m_dnsResolutionService.GetOwnHostName()}";

			m_eventBus.ConnectReceiveEndpoint(queueName, ep =>
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
