using System.Collections.Concurrent;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Caching.Cache.UnderlyingCache.Locking.Interface;

namespace MooMed.Caching.Cache.UnderlyingCache.Locking
{
	public class SemaphoreCacheLockManager<TKeyType> : ICacheLockManager<TKeyType>
	{
		// It is possible that there are locks in here which were attached to a LockedCacheItem which was already disposed
		// but this shouldn't matter.
		private readonly ConcurrentDictionary<TKeyType, ICacheLock> _cacheLocks;

		public SemaphoreCacheLockManager()
		{
			_cacheLocks = new ConcurrentDictionary<TKeyType, ICacheLock>();
		}

		[ItemNotNull]
		public async Task<ICacheLock> GetLockedLock(TKeyType key)
		{
			var semaphoreCacheLock = _cacheLocks.GetOrAdd(key, new SemaphoreCacheLock());

			await semaphoreCacheLock.Lock();

			return semaphoreCacheLock;
		}

		[NotNull]
		public ICacheLock GetUnlockedLock(TKeyType key)
		{
			var semaphoreCacheLock = _cacheLocks.GetOrAdd(key, new SemaphoreCacheLock());

			semaphoreCacheLock.Unlock();

			return semaphoreCacheLock;
		}

		public bool HasLockedLock(TKeyType key)
		{
			var hasCacheLock = _cacheLocks.TryGetValue(key, out var cacheLock);

			return hasCacheLock && cacheLock.IsLocked();
		}

		public void RemoveLock(TKeyType key)
		{
			_cacheLocks.TryRemove(key, out _);
		}
	}
}
