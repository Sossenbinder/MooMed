using Autofac;
using Autofac.Features.AttributeFilters;
using MooMed.Caching.Cache.CacheInformation;
using MooMed.Caching.Cache.Factory;
using MooMed.Caching.Cache.Factory.Interface;
using MooMed.Caching.Cache.UnderlyingCache;
using MooMed.Common.Definitions.IPC;
using MooMed.DotNet.Utils.Async;
using MooMed.Identity.Service.Interface;
using StackExchange.Redis;

namespace MooMed.Caching.Module
{
	public class CachingModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder.RegisterType<CacheSettingsProvider>()
				.As<CacheSettingsProvider>()
				.SingleInstance();

			builder.RegisterType<RedisCacheProvider>()
				.AsSelf()
				.SingleInstance();

			builder.RegisterType<MemoryCacheProvider>()
				.AsSelf()
				.SingleInstance();

			builder.RegisterType<DistributedCacheFactory>()
				.As<IDistributedCacheFactory>()
				.SingleInstance();

			builder.RegisterType<LocalCacheFactory>()
				.As<ILocalCacheFactory>()
				.SingleInstance();

			builder.Register(ctx =>
			{
				var endpointResolver = ctx.Resolve<IEndpointDiscoveryService>();
				return new AsyncLazy<IConnectionMultiplexer>(async () => await ConnectionMultiplexer.ConnectAsync(endpointResolver.GetDeploymentEndpoint(DeploymentService.Redis).DnsName));
			})
			.As<AsyncLazy<IConnectionMultiplexer>>()
			.SingleInstance();
		}
	}
}