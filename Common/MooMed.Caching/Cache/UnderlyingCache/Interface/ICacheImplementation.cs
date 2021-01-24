using System.Threading.Tasks;
using MooMed.Caching.Cache.UnderlyingCache.Locking;

namespace MooMed.Caching.Cache.UnderlyingCache.Interface
{
	public interface ICacheImplementation<in TKey, TValue>
	{
		ValueTask PutItem(TKey key, TValue value, int? secondsToLive = null);

		ValueTask<TValue> GetItem(TKey key);

		ValueTask<LockedCacheItem<TValue>> GetItemLocked(TKey key);

		ValueTask Remove(TKey key);

		ValueTask<bool> HasValue(TKey key);
	}
}