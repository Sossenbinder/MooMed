using JetBrains.Annotations;
using MooMed.Core.Code.Configuration.Interface;

namespace MooMed.Caching.Cache.CacheInformation
{
	public class CacheSettingsProvider
	{
		private static int s_defaultTtlInSeconds;
		
		public CacheSettingsProvider([NotNull] IConfigSettingsProvider settingsProvider)
		{
			s_defaultTtlInSeconds = settingsProvider.ReadValueOrFail<int>("MooMed_Cache_BaseTtlInSeconds");
		}

		public static CacheSettings DefaultCacheSettings = new CacheSettings(s_defaultTtlInSeconds, "DefaultCache");

		public static CacheSettings ProxyCacheSettings = new CacheSettings(s_defaultTtlInSeconds, "ProxyCache");

		public static CacheSettings PartitionInfoCacheSettings = new CacheSettings(s_defaultTtlInSeconds, "PartitionInfoCache");

	}
}
