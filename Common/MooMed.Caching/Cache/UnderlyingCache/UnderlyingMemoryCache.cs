using System;
using Microsoft.Extensions.Caching.Memory;
using MooMed.Caching.Cache.UnderlyingCache.Locking;

namespace MooMed.Caching.Cache.UnderlyingCache
{
	public class UnderlyingMemoryCache<TKeyType, TValueType> : AbstractUnderlyingCache<TKeyType, TValueType>
	{
		private readonly IMemoryCache m_memoryCache;

		private readonly int m_baseTTLTimeInSeconds;

		public UnderlyingMemoryCache(int baseTTLTimeInSeconds)
			:base(new SemaphoreCacheLockManager<TKeyType>())
		{
			m_memoryCache = new MemoryCache(new MemoryCacheOptions());

			m_baseTTLTimeInSeconds = baseTTLTimeInSeconds;
		}

		public override void PutItem(TKeyType key, TValueType value, int? secondsToLive = null)
		{
			// TODO: Make this lock-safe
			m_memoryCache.Set(key, value, /*DateTimeOffset.Now.AddSeconds(secondsToLive ?? m_baseTTLTimeInSeconds)*/ DateTimeOffset.Now.AddSeconds(6000));
		}

		public override TValueType GetItem(TKeyType key)
		{
			var value = (TValueType)m_memoryCache.Get(key);

			return value == null ? default : value;
		}

		public override void Remove(TKeyType key)
		{
			m_memoryCache.Remove(key);
			CacheLockManager.RemoveLock(key);
		}

		public override bool HasValue(TKeyType key)
		{
			return m_memoryCache.TryGetValue(key, out _);
		}
	}
}
