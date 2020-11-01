using Autofac.Features.AttributeFilters;
using JetBrains.Annotations;
using MooMed.Caching.Cache.CacheImplementations;
using MooMed.Caching.Cache.CacheImplementations.Interface;
using MooMed.Caching.Cache.CacheInformation;
using MooMed.Caching.Cache.Factory.Interface;
using MooMed.Caching.Cache.UnderlyingCache;

namespace MooMed.Caching.Cache.Factory
{
	public class AbstractCacheFactory : ICacheFactory
	{
		private readonly IUnderlyingCacheProvider _underlyingCacheProvider;

		private readonly CacheSettingsProvider _cacheSettingsProvider;

		public AbstractCacheFactory(
			IUnderlyingCacheProvider underlyingCacheProvider,
			CacheSettingsProvider cacheSettingsProvider)
		{
			_underlyingCacheProvider = underlyingCacheProvider;
			_cacheSettingsProvider = cacheSettingsProvider;
		}

		public ICache<TValue> CreateCache<TValue>(CacheSettings? cacheSettings = null)
			where TValue : class
		{
			cacheSettings ??= _cacheSettingsProvider.DefaultCacheSettings;

			return new Cache<TValue>(_underlyingCacheProvider.CreateCache<string, TValue>(cacheSettings));
		}

		public ICache<TKey, TValue> CreateCache<TKey, TValue>(CacheSettings? cacheSettings = null)
			where TValue : class
		{
			cacheSettings ??= _cacheSettingsProvider.DefaultCacheSettings;

			return new Cache<TKey, TValue>(_underlyingCacheProvider.CreateCache<TKey, TValue>(cacheSettings));
		}
	}

	public class LocalCacheFactory : AbstractCacheFactory, ILocalCacheFactory
	{
		public LocalCacheFactory(
			MemoryCacheProvider underlyingCacheProvider,
			CacheSettingsProvider cacheSettingsProvider)
			: base(underlyingCacheProvider, cacheSettingsProvider)
		{ }
	}

	public class DistributedCacheFactory : AbstractCacheFactory, IDistributedCacheFactory
	{
		public DistributedCacheFactory(
			RedisCacheProvider underlyingCacheProvider,
			CacheSettingsProvider cacheSettingsProvider)
			: base(underlyingCacheProvider, cacheSettingsProvider)
		{ }
	}
}