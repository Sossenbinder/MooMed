using System.Threading.Tasks;
using MooMed.Caching.Cache.UnderlyingCache.Interface;
using MooMed.Caching.Cache.UnderlyingCache.Locking;
using MooMed.Caching.Cache.UnderlyingCache.Locking.Interface;

namespace MooMed.Caching.Cache.UnderlyingCache
{
	public abstract class AbstractUnderlyingCache<TKey, TValue> : IUnderlyingCache<TKey, TValue>
	{
		protected readonly ICacheLockManager<TKey> CacheLockManager;

		protected AbstractUnderlyingCache(ICacheLockManager<TKey> cacheLockManager)
		{
			CacheLockManager = cacheLockManager;
		}

		public abstract ValueTask PutItem(TKey key, TValue value, int? secondsToLive = null);

		public abstract ValueTask<TValue> GetItem(TKey key);

		public async ValueTask<LockedCacheItem<TValue>> GetItemLocked(TKey key)
		{
			var cacheLock = await CacheLockManager.GetLockedLock(key);

			return new LockedCacheItem<TValue>(await GetItem(key), cacheLock);
		}

		public abstract ValueTask Remove(TKey key);

		public abstract ValueTask<bool> HasValue(TKey key);
	}
}