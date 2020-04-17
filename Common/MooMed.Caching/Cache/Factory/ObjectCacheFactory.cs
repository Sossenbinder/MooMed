﻿using JetBrains.Annotations;
using MooMed.Caching.Cache.CacheImplementations;
using MooMed.Caching.Cache.CacheImplementations.Interface;
using MooMed.Caching.Cache.CacheInformation;
using MooMed.Core.Code.Configuration.Interface;

namespace MooMed.Caching.Cache.Factory
{
    public class ObjectCacheFactory : IDefaultCacheFactory
    {
	    [NotNull]
        private readonly CacheSettingsProvider _cacheSettingsProvider;

        public ObjectCacheFactory([NotNull] CacheSettingsProvider cacheSettingsProvider)
        {
	        _cacheSettingsProvider = cacheSettingsProvider;
        }

        [NotNull]
        public ICache<TDataType> CreateCache<TDataType>([CanBeNull] CacheSettings cacheSettings = null) where TDataType : class
        {
	        if (cacheSettings == null)
	        {
		        cacheSettings = _cacheSettingsProvider.DefaultCacheSettings;
	        }

            return new ObjectCache<TDataType>(cacheSettings);
        }

        [NotNull]
        public ICache<TKey, TDataType> CreateCache<TKey, TDataType>([CanBeNull] CacheSettings cacheSettings = null) where TDataType : class
		{
			if (cacheSettings == null)
			{
				cacheSettings = _cacheSettingsProvider.DefaultCacheSettings;
			}

			return new ObjectCache<TKey, TDataType>(cacheSettings);
        }
    }
}
