using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MooMed.Caching.Cache.UnderlyingCache.Locking;

namespace MooMed.Caching.Cache.CacheImplementations.Interface
{
	public interface ICache<TDataType> : ICache<string, TDataType> { }

	public interface ICache<in TKeyType, TDataType>
	{
		ValueTask PutItem(TKeyType key, TDataType value, int? secondsToLive = null);

		[return: MaybeNull]
		ValueTask<TDataType> GetItem(TKeyType key);

		ValueTask<LockedCacheItem<TDataType>> GetItemLocked(TKeyType key);

		Task PutItems(TKeyType key, IEnumerable<TDataType> values, int? secondsToLive = null);

		ValueTask Remove(TKeyType key);

		ValueTask<bool> HasValue(TKeyType key);
	}
}