using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using MooMed.Caching.Cache.UnderlyingCache.Locking.Interface;

namespace MooMed.Caching.Cache.UnderlyingCache.Locking
{
	public abstract class AbstractCacheLockManager<TKey> : ICacheLockManager<TKey>
		where TKey : notnull
	{
		// It is possible that there are locks in here which were attached to a LockedCacheItem which was already disposed
		// but this shouldn't matter.
		private readonly ConcurrentDictionary<TKey, ICacheLock> _cacheLocks;

		private readonly Func<string, ICacheLock> _cacheLockGenerator;

		protected AbstractCacheLockManager(Func<string, ICacheLock> cacheLockGenerator)
		{
			_cacheLocks = new ConcurrentDictionary<TKey, ICacheLock>();
			_cacheLockGenerator = cacheLockGenerator;
		}

		public async Task<ICacheLock> GetLockedLock(TKey key)
		{
			var semaphoreCacheLock = _cacheLocks.GetOrAdd(key, _cacheLockGenerator(key.ToString()!));

			await semaphoreCacheLock.Lock();

			return semaphoreCacheLock;
		}

		public async Task<ICacheLock> GetUnlockedLock(TKey key)
		{
			var semaphoreCacheLock = _cacheLocks.GetOrAdd(key, _cacheLockGenerator(key.ToString()!));

			if (await semaphoreCacheLock.IsLocked())
			{
				await semaphoreCacheLock.Unlock();
			}

			return semaphoreCacheLock;
		}

		public async Task<bool> HasLockedLock(TKey key)
		{
			var hasCacheLock = _cacheLocks.TryGetValue(key, out var cacheLock);

			return hasCacheLock && await cacheLock!.IsLocked();
		}

		public void RemoveLock(TKey key) => _cacheLocks.TryRemove(key, out _);
	}
}