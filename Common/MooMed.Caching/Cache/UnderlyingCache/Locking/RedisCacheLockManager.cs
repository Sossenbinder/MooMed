using MooMed.DotNet.Utils.Async;
using StackExchange.Redis;

namespace MooMed.Caching.Cache.UnderlyingCache.Locking
{
	public class RedisCacheLockManager<TKey> : AbstractCacheLockManager<TKey>
		where TKey : notnull
	{
		public RedisCacheLockManager(AsyncLazy<IConnectionMultiplexer> connectionMultiPlexer)
			: base(key => new RedisCacheLock(connectionMultiPlexer, key))
		{ }
	}
}