using JetBrains.Annotations;
using MooMed.Caching.Cache.CacheImplementations.Interface;
using MooMed.Caching.Cache.CacheInformation;

namespace MooMed.Caching.Cache.CacheImplementations
{
	public class ValueCache<TValue> : ValueCache<string, TValue>, ICache<TValue> where TValue : struct
	{
		public ValueCache([NotNull] CacheSettings cacheSettings) 
			: base(cacheSettings)
		{
		}
	}

	public class ValueCache<TKey, TValue> 
		: AbstractCache<TKey, TValue> where TValue : struct
	{
		protected ValueCache([NotNull] CacheSettings cacheSettings) 
			: base(cacheSettings)
		{
		}
	}
}
