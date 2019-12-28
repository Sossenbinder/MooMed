using System;
using JetBrains.Annotations;
using MooMed.Caching.Cache.UnderlyingCache.Locking.Interface;

namespace MooMed.Caching.Cache.UnderlyingCache.Locking
{
	public class LockedCacheItem<T> : IDisposable
	{
		private readonly ICacheLock m_cacheLock;

		public T Payload { get; }

		public LockedCacheItem(
			T payload, 
			[NotNull] ICacheLock cacheLock)
		{
			m_cacheLock = cacheLock;
			Payload = payload;
		}

		public void Release()
		{
			m_cacheLock.Unlock();
		}

		public void Dispose()
		{
			Release();
		}
	}
}
