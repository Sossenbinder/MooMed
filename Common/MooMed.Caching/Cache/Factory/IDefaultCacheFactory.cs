using MooMed.Caching.Cache.CacheImplementations.Interface;
using MooMed.Caching.Cache.CacheInformation;

namespace MooMed.Caching.Cache.Factory
{
    public interface IDefaultCacheFactory
    {
        ICache<TDataType> CreateCache<TDataType>(CacheSettings cacheSettings = null) where TDataType : class;

        ICache<TKey, TDataType> CreateCache<TKey, TDataType>(CacheSettings cacheSettings = null) where TDataType : class;

    }
}
