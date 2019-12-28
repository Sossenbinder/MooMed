using System.Threading.Tasks;

namespace MooMed.Caching.Cache.UnderlyingCache.Locking.Interface
{
	public interface ICacheLockManager<in TKeyType>
	{
		Task<ICacheLock> GetLockedLock(TKeyType key);

		ICacheLock GetUnlockedLock(TKeyType key);

		bool HasLockedLock(TKeyType key);

		void RemoveLock(TKeyType key);

	}
}
