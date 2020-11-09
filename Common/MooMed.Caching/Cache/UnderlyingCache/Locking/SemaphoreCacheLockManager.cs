﻿namespace MooMed.Caching.Cache.UnderlyingCache.Locking
{
	public class SemaphoreCacheLockManager<TKey> : AbstractCacheLockManager<TKey>
		where TKey : notnull
	{
		public SemaphoreCacheLockManager()
			: base(_ => new SemaphoreCacheLock())
		{ }
	}
}