namespace MooMed.Caching.Cache.UnderlyingCache.Locking
{
	public class SemaphoreCacheLockManager<TKey> : AbstractCacheLockManager<TKey>
	{
		public SemaphoreCacheLockManager()
			: base(_ => new SemaphoreCacheLock())
		{ }
	}
}