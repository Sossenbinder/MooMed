using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using MooMed.Caching.Cache.UnderlyingCache.Locking;

namespace MooMed.Caching.Cache.UnderlyingCache
{
	public class UnderlyingMemoryCache<TKeyType, TValueType> : AbstractUnderlyingCache<TKeyType, TValueType>
	{
		private readonly IMemoryCache _memoryCache;

		private readonly int _baseTtlTimeInSeconds;

		public UnderlyingMemoryCache(int baseTtlTimeInSeconds)
			: base(new SemaphoreCacheLockManager<TKeyType>())
		{
			_memoryCache = new MemoryCache(new MemoryCacheOptions());

			_baseTtlTimeInSeconds = baseTtlTimeInSeconds;
		}

		public override void PutItem(TKeyType key, TValueType value, int? secondsToLive = null)
		{
			_memoryCache.Set(key, value, DateTimeOffset.Now.AddSeconds(secondsToLive ?? _baseTtlTimeInSeconds));
		}

		public override TValueType GetItem(TKeyType key)
		{
			var value = (TValueType)_memoryCache.Get(key);

			return value == null ? default : value;
		}

		public override void Remove(TKeyType key)
		{
			_memoryCache.Remove(key);
			CacheLockManager.RemoveLock(key);
		}

		public override bool HasValue(TKeyType key)
		{
			return _memoryCache.TryGetValue(key, out _);
		}
	}
}