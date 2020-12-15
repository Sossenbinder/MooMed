using System;
using JetBrains.Annotations;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.DependencyInjection;
using MooMed.Common.Definitions.IPC;
using MooMed.Core.Code.Helper.Retry;
using MooMed.Identity.Service.Interface;
using MooMed.Logging.Abstractions.Interface;

namespace MooMed.Eventing.Helper
{
    public static class MassTransitBusFactory
    {
        /// <summary>
        /// Creates a MT Bus and gets the required service for initialization
        /// </summary>
        public static IBusControl CreateBus(
            [NotNull] IServiceProvider provider,
            Action<IRabbitMqBusFactoryConfigurator>? configFunc = null)
        {
            var endpointDiscoveryService = provider.GetRequiredService<IEndpointDiscoveryService>();
            var logger = provider.GetRequiredService<IMooMedLogger>();

            return CreateBus(endpointDiscoveryService, logger, configFunc);
        }

        /// <summary>
        /// Creates a MT Bus with retry logic included (in case the RabbitMQ endpoint starts up later)
        /// </summary>
        public static IBusControl CreateBus(
            [NotNull] IEndpointDiscoveryService endpointDiscoveryService,
            [NotNull] IMooMedLogger logger,
            Action<IRabbitMqBusFactoryConfigurator>? configFunc = null)
        {
            return Bus.Factory.CreateUsingRabbitMq(async config =>
            {
                config.Durable = false;
                config.AutoDelete = true;
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
                });

                configFunc?.Invoke(config);
            });
        }
    }
}