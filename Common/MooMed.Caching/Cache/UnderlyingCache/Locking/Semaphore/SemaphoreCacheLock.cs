using System;
using System.Threading;
using System.Threading.Tasks;
using MooMed.Caching.Cache.UnderlyingCache.Locking.Interface;

namespace MooMed.Caching.Cache.UnderlyingCache.Locking.Semaphore
{
	/// <summary>
	/// Implementation of a cache lock with SemaphoreSlim
	/// </summary>
	public class SemaphoreCacheLock : ICacheLock
	{
		private readonly SemaphoreSlim _lockingSemaphoreSlim;

		private readonly TimeSpan _lockTimeout;

		public SemaphoreCacheLock()
		{
			_lockingSemaphoreSlim = new SemaphoreSlim(1, 1);
			_lockTimeout = TimeSpan.FromSeconds(5);
		}

		/// <summary>
		/// Whoever asked for the lock will have at most _lockTimeout seconds before the lock is released
		/// </summary>
		/// <returns></returns>
		public Task Lock() => _lockingSemaphoreSlim.WaitAsync(_lockTimeout);

		public Task Unlock()
		{
			_lockingSemaphoreSlim.Release();
			return Task.CompletedTask;
		}

		public Task<bool> IsLocked()
		{
			return Task.FromResult(_lockingSemaphoreSlim.CurrentCount == 0);
		}
	}
}