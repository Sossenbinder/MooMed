using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MassTransit;
using MassTransit.RabbitMqTransport;
using MooMed.Common.Definitions.IPC;
using MooMed.Core.Code.Helper.Retry;
using MooMed.Core.Code.Logging.Loggers.Interface;
using MooMed.Dns.Service.Interface;

namespace MooMed.Eventing.Helper
{
	public static class MassTransitBusFactory
	{
		public static IBusControl CreateBus(
			[NotNull] IServiceProvider provider,
			[CanBeNull] Action<IRabbitMqBusFactoryConfigurator> configFunc = null)
		{
			var dnsResolutionService = (IDnsResolutionService)provider.GetService(typeof(IDnsResolutionService));
			var logger = (IMainLogger)provider.GetService(typeof(IMainLogger));

			return CreateBus(dnsResolutionService, logger, configFunc);
		}

		public static IBusControl CreateBus(
			[NotNull] IDnsResolutionService dnsResolutionService,
			[NotNull] IMainLogger logger,
			[CanBeNull] Action<IRabbitMqBusFactoryConfigurator> configFunc = null)
		{
			return Bus.Factory.CreateUsingRabbitMq(async config =>
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

				configFunc?.Invoke(config);
			});
		}
	}
}
