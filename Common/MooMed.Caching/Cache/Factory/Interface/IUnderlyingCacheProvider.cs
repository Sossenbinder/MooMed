using MooMed.Caching.Cache.CacheInformation;
using MooMed.Caching.Cache.UnderlyingCache.Interface;

namespace MooMed.Caching.Cache.Factory.Interface
{
	public interface IUnderlyingCacheProvider
	{
		ICacheImplementation<TKey, TValue> CreateCache<TKey, TValue>(CacheSettings cacheSettings);
	}
}