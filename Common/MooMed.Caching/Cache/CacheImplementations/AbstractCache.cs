using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Caching.Cache.CacheImplementations.Interface;
using MooMed.Caching.Cache.CacheInformation;
using MooMed.Caching.Cache.UnderlyingCache;
using MooMed.Caching.Cache.UnderlyingCache.Interface;
using MooMed.Caching.Cache.UnderlyingCache.Locking;

namespace MooMed.Caching.Cache.CacheImplementations
{
	public abstract class AbstractCache<TKeyType, TDataType> : ICache<TKeyType, TDataType>
	{
		[NotNull]
		private readonly IUnderlyingCache<TKeyType, TDataType> m_memoryCache;

		protected AbstractCache([NotNull] CacheSettings cacheSettings)
		{
			m_memoryCache = new UnderlyingMemoryCache<TKeyType, TDataType>(cacheSettings.TtlInSeconds);
		}

		public void PutItem(TKeyType key, TDataType value, int? secondsToLive = null)
		{
			m_memoryCache.PutItem(key, value, secondsToLive);
		}

		public async Task<LockedCacheItem<TDataType>> GetItemLocked(TKeyType key)
		{
			return await m_memoryCache.GetItemLocked(key);
		}

		public void PutItems(TKeyType key, IEnumerable<TDataType> values, int? secondsToLive = null)
		{
			foreach (var value in values)
			{
				m_memoryCache.PutItem(key, value, secondsToLive);
			}
		}

		public void Remove(TKeyType key)
		{
			m_memoryCache.Remove(key);
		}

		public bool HasValue(TKeyType key)
		{
			return m_memoryCache.HasValue(key);
		}

		[CanBeNull]
		public TDataType this[[NotNull] TKeyType key]
		{
			set => PutItem(key, value);
			get => GetItem(key);
		}

		public TDataType GetItem(TKeyType key)
		{
			return m_memoryCache.GetItem(key);
		}

	}
}
