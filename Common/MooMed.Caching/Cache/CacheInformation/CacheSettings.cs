using JetBrains.Annotations;

namespace MooMed.Caching.Cache.CacheInformation
{
	public class CacheSettings
	{
		public int TtlInSeconds { get; set; }

		public string Name { get; set; }

		public CacheSettings(int ttlInSeconds, [NotNull] string name)
		{
			TtlInSeconds = ttlInSeconds;
			Name = name;
		}
	}
}
