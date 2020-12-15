using MooMed.Common.Definitions.Configuration;

namespace MooMed.Caching.Cache.CacheInformation
{
    public class CacheSettingsProvider
    {
        private static int _defaultTtlInSeconds;

        public CacheSettingsProvider(IConfigProvider provider)
        {
            _defaultTtlInSeconds = provider.ReadValueOrFail<int>("MooMed_Cache_BaseTtlInSeconds");
        }

        public CacheSettings DefaultCacheSettings => new(_defaultTtlInSeconds, "DefaultCache");

        public CacheSettings ProxyCacheSettings => new(_defaultTtlInSeconds, "ProxyCache");

        public CacheSettings PartitionInfoCacheSettings => new(_defaultTtlInSeconds, "PartitionInfoCache");
    }
}