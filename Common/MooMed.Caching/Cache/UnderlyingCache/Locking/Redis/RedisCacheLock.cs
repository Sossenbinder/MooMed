using System;
using System.Threading.Tasks;
using MooMed.Caching.Cache.UnderlyingCache.Locking.Interface;
using MooMed.DotNet.Utils.Async;
using StackExchange.Redis;

namespace MooMed.Caching.Cache.UnderlyingCache.Locking.Redis
{
	public class RedisCacheLock : ICacheLock
	{
		private readonly AsyncLazy<IConnectionMultiplexer> _connectionMultiPlexer;

		private readonly string _key;

		private readonly TimeSpan _lockTimeout;

		private readonly string _token;

		public RedisCacheLock(
			AsyncLazy<IConnectionMultiplexer> connectionMultiplexer,
			string key)
		{
			_connectionMultiPlexer = connectionMultiplexer;
			_key = $"lock_{key}";
			_lockTimeout = TimeSpan.FromSeconds(5);

			_token = Guid.NewGuid().ToString();
		}

		public async Task Lock()
		{
			var db = await GetDb();

			await db.LockTakeAsync(_key, _token, _lockTimeout);
		}

		public async Task Unlock()
		{
			var db = await GetDb();

			await db.LockReleaseAsync(_key, _token);
		}

		public async Task<bool> IsLocked()
		{
			var db = await GetDb();

			var lockVal = await db.LockQueryAsync(_key);

			return lockVal.HasValue;
		}

		private async ValueTask<IDatabase> GetDb() => (await _connectionMultiPlexer.Value).GetDatabase();
	}
}