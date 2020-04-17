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
		private readonly IUnderlyingCache<TKeyType, TDataType> _memoryCache;

		protected AbstractCache([NotNull] CacheSettings cacheSettings)
		{
			_memoryCache = new UnderlyingMemoryCache<TKeyType, TDataType>(cacheSettings.TtlInSeconds);
		}

		public void PutItem(TKeyType key, TDataType value, int? secondsToLive = null)
		{
			_memoryCache.PutItem(key, value, secondsToLive);
		}

		public async Task<LockedCacheItem<TDataType>> GetItemLocked(TKeyType key)
		{
			return await _memoryCache.GetItemLocked(key);
		}

		public void PutItems(TKeyType key, IEnumerable<TDataType> values, int? secondsToLive = null)
		{
			foreach (var value in values)
			{
				_memoryCache.PutItem(key, value, secondsToLive);
			}
		}

		public void Remove(TKeyType key)
		{
			_memoryCache.Remove(key);
		}

		public bool HasValue(TKeyType key)
		{
			return _memoryCache.HasValue(key);
		}

		[CanBeNull]
		public TDataType this[[NotNull] TKeyType key]
		{
			set => PutItem(key, value);
			get => GetItem(key);
		}

		public TDataType GetItem(TKeyType key)
		{
			return _memoryCache.GetItem(key);
		}

	}
}
