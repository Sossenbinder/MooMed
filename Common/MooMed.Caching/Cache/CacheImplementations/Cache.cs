using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Caching.Cache.CacheImplementations.Interface;
using MooMed.Caching.Cache.UnderlyingCache.Interface;
using MooMed.Caching.Cache.UnderlyingCache.Locking;

namespace MooMed.Caching.Cache.CacheImplementations
{
	public class Cache<TValue> : Cache<string, TValue>, ICache<TValue>
	{
		public Cache(IUnderlyingCache<string, TValue> underlyingCache)
			: base(underlyingCache)
		{
		}
	}

	public class Cache<TKey, TValue> : ICache<TKey, TValue>
	{
		[NotNull]
		private readonly IUnderlyingCache<TKey, TValue> _underlyingCache;

		public Cache(IUnderlyingCache<TKey, TValue> underlyingCache)
		{
			_underlyingCache = underlyingCache;
		}

		public ValueTask PutItem(TKey key, TValue value, int? secondsToLive = null)
			=> _underlyingCache.PutItem(key, value, secondsToLive);

		public ValueTask<LockedCacheItem<TValue>> GetItemLocked(TKey key)
			=> _underlyingCache.GetItemLocked(key);

		public async Task PutItems(TKey key, IEnumerable<TValue> values, int? secondsToLive = null)
		{
			await Task.WhenAll(values.Select(value => _underlyingCache.PutItem(key, value, secondsToLive).AsTask()));
		}

		public ValueTask Remove(TKey key)
			=> _underlyingCache.Remove(key);

		public ValueTask<bool> HasValue(TKey key)
			=> _underlyingCache.HasValue(key);

		public ValueTask<TValue> GetItem(TKey key)
			=> _underlyingCache.GetItem(key);
	}
}