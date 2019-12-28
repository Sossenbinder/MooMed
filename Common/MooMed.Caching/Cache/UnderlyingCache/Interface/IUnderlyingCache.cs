using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Caching.Cache.UnderlyingCache.Locking;

namespace MooMed.Caching.Cache.UnderlyingCache.Interface
{
	public interface IUnderlyingCache<in TKeyType, TValueType>
	{
		void PutItem([NotNull] TKeyType key, [NotNull] TValueType value, [CanBeNull] int? secondsToLive = null);

		[CanBeNull]
		TValueType GetItem([NotNull] TKeyType key);

		[ItemNotNull]
		Task<LockedCacheItem<TValueType>> GetItemLocked([NotNull] TKeyType key);

		void Remove([NotNull] TKeyType key);

		bool HasValue([NotNull] TKeyType key);
	}
}
