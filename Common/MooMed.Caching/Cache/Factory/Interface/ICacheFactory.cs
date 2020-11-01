using MooMed.Caching.Cache.CacheImplementations.Interface;
using MooMed.Caching.Cache.CacheInformation;

namespace MooMed.Caching.Cache.Factory.Interface
{
	public interface ICacheFactory
	{
		ICache<TDataType> CreateCache<TDataType>(CacheSettings? cacheSettings = null) where TDataType : class;

		ICache<TKey, TDataType> CreateCache<TKey, TDataType>(CacheSettings? cacheSettings = null) where TDataType : class;
	}

	public interface ILocalCacheFactory : ICacheFactory { }

	public interface IDistributedCacheFactory : ICacheFactory { }
}