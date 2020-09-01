using JetBrains.Annotations;
using MooMed.Common.Definitions.Configuration;

namespace MooMed.Caching.Cache.CacheInformation
{
	public class CacheSettingsProvider
	{
		private static int _defaultTtlInSeconds;

		public CacheSettingsProvider([NotNull] IConfigProvider provider)
		{
			_defaultTtlInSeconds = provider.ReadValueOrFail<int>("MooMed_Cache_BaseTtlInSeconds");
		}

		public CacheSettings DefaultCacheSettings => new CacheSettings(_defaultTtlInSeconds, "DefaultCache");

		public CacheSettings ProxyCacheSettings => new CacheSettings(_defaultTtlInSeconds, "ProxyCache");

		public CacheSettings PartitionInfoCacheSettings => new CacheSettings(_defaultTtlInSeconds, "PartitionInfoCache");
	}
}