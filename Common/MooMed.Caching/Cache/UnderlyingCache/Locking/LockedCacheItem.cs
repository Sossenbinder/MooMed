using System;
using System.Threading;
using System.Threading.Tasks;
using MooMed.Caching.Cache.UnderlyingCache.Locking.Interface;
using MooMed.DotNet.Extensions;

namespace MooMed.Caching.Cache.UnderlyingCache.Locking
{
	/// <summary>
	/// A locked cache item containing lock and payload
	/// </summary>
	/// <typeparam name="TKey">Type of cache key</typeparam>
	/// <typeparam name="TValue">Type of cache value</typeparam>
	public class LockedCacheItem<TValue> : IAsyncDisposable
	{
		public TValue Payload { get; }

		private bool _disposed;

		private readonly ICacheLock _cacheLock;

		private readonly CancellationTokenSource _cancellationTokenSource;

		public LockedCacheItem(
			TValue payload,
			ICacheLock cacheLock)
		{
			_cacheLock = cacheLock;
			Payload = payload;

			_disposed = false;
			_cancellationTokenSource = new CancellationTokenSource();

			RunAutoReleaseTimer().IgnoreTaskCancelledException();
		}

		private async Task RunAutoReleaseTimer()
		{
			await Task.Delay(TimeSpan.FromSeconds(5), _cancellationTokenSource.Token);

			if (!_cancellationTokenSource.Token.IsCancellationRequested)
			{
				await Release();
			}
		}

		public void Dispose() => DisposeAsync().GetAwaiter().GetResult();

		public async ValueTask DisposeAsync() => await Release();

		public async Task Release()
		{
			// This could potentially be called explicitly even after the auto-dispose, so better check
			if (_disposed)
			{
				return;
			}

			await _cacheLock.Unlock();
			_disposed = true;
			_cancellationTokenSource.Cancel();
		}
	}
}