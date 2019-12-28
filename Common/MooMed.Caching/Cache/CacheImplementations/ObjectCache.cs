using JetBrains.Annotations;
using MooMed.Caching.Cache.CacheImplementations.Interface;
using MooMed.Caching.Cache.CacheInformation;

namespace MooMed.Caching.Cache.CacheImplementations
{
	public class ObjectCache<TValue> : ObjectCache<string, TValue>, ICache<TValue> where TValue : class
	{
		public ObjectCache([NotNull] CacheSettings cacheSettings)
			: base(cacheSettings)
		{
		}
	}

	public class ObjectCache<TKey, TValue>
		: AbstractCache<TKey, TValue> where TValue : class
	{
		public ObjectCache([NotNull] CacheSettings cacheSettings)
			: base(cacheSettings)
		{
		}
	}
}
