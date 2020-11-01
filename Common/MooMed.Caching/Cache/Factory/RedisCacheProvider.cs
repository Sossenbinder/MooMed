using MooMed.Caching.Cache.CacheInformation;
using MooMed.Caching.Cache.Factory.Interface;
using MooMed.Caching.Cache.UnderlyingCache;
using MooMed.Caching.Cache.UnderlyingCache.Interface;
using MooMed.DotNet.Utils.Async;
using MooMed.Serialization.Service.Interface;
using StackExchange.Redis;

namespace MooMed.Caching.Cache.Factory
{
	public class RedisCacheProvider : IUnderlyingCacheProvider
	{
		private readonly AsyncLazy<IConnectionMultiplexer> _connectionMultiPlexer;

		private readonly ISerializationService _serializationService;

		public RedisCacheProvider(
			AsyncLazy<IConnectionMultiplexer> connectionMultiPlexer,
			ISerializationService serializationService)
		{
			_connectionMultiPlexer = connectionMultiPlexer;
			_serializationService = serializationService;
		}

		public IUnderlyingCache<TKey, TValue> CreateCache<TKey, TValue>(CacheSettings cacheSettings)
		{
			return new UnderlyingRedisCache<TKey, TValue>(_connectionMultiPlexer, _serializationService);
		}
	}
}