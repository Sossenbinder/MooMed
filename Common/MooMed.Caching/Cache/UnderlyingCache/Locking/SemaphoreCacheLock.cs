using System.Threading;
using System.Threading.Tasks;
using MooMed.Caching.Cache.UnderlyingCache.Locking.Interface;

namespace MooMed.Caching.Cache.UnderlyingCache.Locking
{
	public class SemaphoreCacheLock : ICacheLock
	{
		private readonly SemaphoreSlim m_lockingSemaphoreSlim;

		public SemaphoreCacheLock()
		{
			m_lockingSemaphoreSlim = new SemaphoreSlim(1, 1);
		}

		public async Task Lock()
		{
			await m_lockingSemaphoreSlim.WaitAsync();
		}

		public void Unlock()
		{
			m_lockingSemaphoreSlim.Release();
		}

		public bool IsLocked()
		{
			return m_lockingSemaphoreSlim.CurrentCount == 0;
		}
	}
}
