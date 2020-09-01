using System;
using JetBrains.Annotations;
using MooMed.Caching.Cache.UnderlyingCache.Locking.Interface;

namespace MooMed.Caching.Cache.UnderlyingCache.Locking
{
	/// <summary>
	/// A locked cache item containing lock and payload
	/// </summary>
	/// <typeparam name="T">Type of cache value</typeparam>
	public class LockedCacheItem<T> : IDisposable
	{
		private readonly ICacheLock _cacheLock;

		public T Payload { get; }

		public LockedCacheItem(
			T payload,
			[NotNull] ICacheLock cacheLock)
		{
			_cacheLock = cacheLock;
			Payload = payload;
		}

		public void Release() => _cacheLock.Unlock();

		public void Dispose() => Release();
	}
}