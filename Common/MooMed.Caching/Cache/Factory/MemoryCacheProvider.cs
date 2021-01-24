using MooMed.Caching.Cache.CacheInformation;
using MooMed.Caching.Cache.Factory.Interface;
using MooMed.Caching.Cache.UnderlyingCache;
using MooMed.Caching.Cache.UnderlyingCache.Interface;

namespace MooMed.Caching.Cache.Factory
{
	public class MemoryCacheProvider : IUnderlyingCacheProvider
	{
		public ICacheImplementation<TKey, TValue> CreateCache<TKey, TValue>(CacheSettings cacheSettings)
		{
			return new MemoryCacheImplementation<TKey, TValue>(cacheSettings.TtlInSeconds);
		}
	}
}