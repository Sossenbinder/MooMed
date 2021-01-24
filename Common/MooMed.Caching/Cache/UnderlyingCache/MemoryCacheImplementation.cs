using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using MooMed.Caching.Cache.UnderlyingCache.Locking.Semaphore;

namespace MooMed.Caching.Cache.UnderlyingCache
{
	public class MemoryCacheImplementation<TKey, TValue> : AbstractCacheImplementation<TKey, TValue>
	{
		private readonly IMemoryCache _memoryCache;

		private readonly int _baseTtlTimeInSeconds;

		public MemoryCacheImplementation(int baseTtlTimeInSeconds)
			: base(new SemaphoreCacheLockManager<TKey>())
		{
			_memoryCache = new MemoryCache(new MemoryCacheOptions());

			_baseTtlTimeInSeconds = baseTtlTimeInSeconds;
		}

		public override ValueTask PutItem(TKey key, TValue value, int? secondsToLive = null)
		{
			_memoryCache.Set(key, value, DateTimeOffset.Now.AddSeconds(secondsToLive ?? _baseTtlTimeInSeconds));

			return ValueTask.CompletedTask;
		}

		public override ValueTask<TValue> GetItem(TKey key)
		{
			var value = _memoryCache.Get<TValue>(key);

			return new ValueTask<TValue>(value == null ? default : value);
		}

		public override ValueTask Remove(TKey key)
		{
			_memoryCache.Remove(key);
			CacheLockManager.RemoveLock(key);

			return ValueTask.CompletedTask;
		}

		public override ValueTask<bool> HasValue(TKey key)
		{
			return new ValueTask<bool>(_memoryCache.TryGetValue(key, out _));
		}
	}
}