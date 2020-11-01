using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MassTransit;
using MassTransit.RabbitMqTransport;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Logging;
using MooMed.Core.Code.Helper.Retry;
using MooMed.Identity.Service.Interface;

namespace MooMed.Eventing.Helper
{
	public static class MassTransitBusFactory
	{
		public static IBusControl CreateBus(
			[NotNull] IServiceProvider provider,
			[CanBeNull] Action<IRabbitMqBusFactoryConfigurator> configFunc = null)
		{
			var endpointDiscoveryService = (IEndpointDiscoveryService)provider.GetService(typeof(IEndpointDiscoveryService));
			var logger = (IMooMedLogger)provider.GetService(typeof(IMooMedLogger));

			return CreateBus(endpointDiscoveryService, logger, configFunc);
		}

		public static IBusControl CreateBus(
			[NotNull] IEndpointDiscoveryService endpointDiscoveryService,
			[NotNull] IMooMedLogger logger,
			[CanBeNull] Action<IRabbitMqBusFactoryConfigurator> configFunc = null)
		{
			return Bus.Factory.CreateUsingRabbitMq(async config =>
			{
				config.PurgeOnStartup = true;

				await RetryStrategy.DoRetryExponential(() =>
				{
					var deploymentIp = endpointDiscoveryService.GetDeploymentEndpoint(DeploymentService.RabbitMq).DnsName;

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