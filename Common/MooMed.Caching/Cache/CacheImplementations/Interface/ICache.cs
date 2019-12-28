using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Caching.Cache.UnderlyingCache.Locking;

namespace MooMed.Caching.Cache.CacheImplementations.Interface
{
	public interface ICache<TDataType> : ICache<string, TDataType>
	{

	}

    public interface ICache<in TKeyType, TDataType>
    {
        void PutItem([NotNull] TKeyType key, [NotNull] TDataType value, [CanBeNull] int? secondsToLive = null);

        [CanBeNull]
        TDataType GetItem([NotNull] TKeyType key);

        [ItemNotNull]
        Task<LockedCacheItem<TDataType>> GetItemLocked([NotNull] TKeyType key);

		void PutItems([NotNull] TKeyType key, [NotNull] [ItemNotNull] IEnumerable<TDataType> values, [CanBeNull] int? secondsToLive = null);

        void Remove([NotNull] TKeyType key);

        bool HasValue([NotNull] TKeyType key);

        TDataType this[TKeyType key] { get; set; }
    }
}
