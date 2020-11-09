using JetBrains.Annotations;

namespace MooMed.Caching.Helper
{
	public static class CacheKeyUtils
	{
		[NotNull]
		public static string GetCacheKeyForAccountId(int accountId)
		{
			return $"a-{accountId}";
		}

		[NotNull]
		public static string GetCacheKeyForSessionContext(int accountId)
		{
			return $"s-a-{accountId}";
		}
	}
}