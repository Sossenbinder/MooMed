using System.Threading.Tasks;
using MooMed.Caching.Cache.UnderlyingCache.Interface;
using MooMed.Caching.Cache.UnderlyingCache.Locking.Interface;

namespace MooMed.Caching.Cache.UnderlyingCache.Locking
{
	public abstract class AbstractUnderlyingCache<TKeyType, TValueType> : IUnderlyingCache<TKeyType, TValueType>
	{
		protected readonly ICacheLockManager<TKeyType> CacheLockManager;

		protected AbstractUnderlyingCache(ICacheLockManager<TKeyType> cacheLockManager)
		{
			CacheLockManager = cacheLockManager;
		}

		public abstract void PutItem(TKeyType key, TValueType value, int? secondsToLive = null);

		public abstract TValueType GetItem(TKeyType key);

		public async Task<LockedCacheItem<TValueType>> GetItemLocked(TKeyType key)
		{
			var semaphoreCacheLock = await CacheLockManager.GetLockedLock(key);

			return new LockedCacheItem<TValueType>(GetItem(key), semaphoreCacheLock);
		}

		public abstract void Remove(TKeyType key);

		public abstract bool HasValue(TKeyType key);
	}
}
