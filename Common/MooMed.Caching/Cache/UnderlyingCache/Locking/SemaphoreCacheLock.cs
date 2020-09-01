using System;
using System.Threading;
using System.Threading.Tasks;
using MooMed.Caching.Cache.UnderlyingCache.Locking.Interface;

namespace MooMed.Caching.Cache.UnderlyingCache.Locking
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

		public Task Lock() => _lockingSemaphoreSlim.WaitAsync(_lockTimeout);

		public void Unlock() => _lockingSemaphoreSlim.Release();

		public bool IsLocked() => _lockingSemaphoreSlim.CurrentCount == 0;
	}
}