using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MooMed.Caching.Cache.CacheImplementations.Interface;
using MooMed.Caching.Cache.UnderlyingCache.Interface;
using MooMed.Caching.Cache.UnderlyingCache.Locking;

namespace MooMed.Caching.Cache.CacheImplementations
{
	public class Cache<TValue> : Cache<string, TValue>, ICache<TValue>
	{
		public Cache(ICacheImplementation<string, TValue> cacheImplementation)
			: base(cacheImplementation)
		{
		}
	}

	public class Cache<TKey, TValue> : ICache<TKey, TValue>
	{
		private readonly ICacheImplementation<TKey, TValue> _cacheImplementation;

		public Cache(ICacheImplementation<TKey, TValue> cacheImplementation)
		{
			_cacheImplementation = cacheImplementation;
		}

		public ValueTask PutItem(TKey key, TValue value, int? secondsToLive = null)
			=> _cacheImplementation.PutItem(key, value, secondsToLive);

		public ValueTask<LockedCacheItem<TValue>> GetItemLocked(TKey key)
			=> _cacheImplementation.GetItemLocked(key);

		public Task PutItems(TKey key, IEnumerable<TValue> values, int? secondsToLive = null)
			=> Task.WhenAll(values.Select(value => _cacheImplementation.PutItem(key, value, secondsToLive).AsTask()));

		public ValueTask Remove(TKey key)
			=> _cacheImplementation.Remove(key);

		public ValueTask<bool> HasValue(TKey key)
			=> _cacheImplementation.HasValue(key);

		public ValueTask<TValue> GetItem(TKey key)
			=> _cacheImplementation.GetItem(key);
	}
}