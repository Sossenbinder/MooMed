using System.Threading;
using System.Threading.Tasks;
using MooMed.Caching.Cache.UnderlyingCache.Locking.Interface;

namespace MooMed.Caching.Cache.UnderlyingCache.Locking
{
	public class SemaphoreCacheLock : ICacheLock
	{
		private readonly SemaphoreSlim _lockingSemaphoreSlim;

		public SemaphoreCacheLock()
		{
			_lockingSemaphoreSlim = new SemaphoreSlim(1, 1);
		}

		public async Task Lock()
		{
			await _lockingSemaphoreSlim.WaitAsync();
		}

		public void Unlock()
		{
			_lockingSemaphoreSlim.Release();
		}

		public bool IsLocked()
		{
			return _lockingSemaphoreSlim.CurrentCount == 0;
		}
	}
}
