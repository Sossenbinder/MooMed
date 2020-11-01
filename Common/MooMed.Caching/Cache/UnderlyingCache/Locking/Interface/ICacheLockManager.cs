using System.Threading.Tasks;

namespace MooMed.Caching.Cache.UnderlyingCache.Locking.Interface
{
	public interface ICacheLockManager<in TKey>
	{
		/// <summary>
		/// Retrieves a locked lock
		/// </summary>
		/// <param name="key">Key of lock</param>
		/// <returns>Locked lock</returns>
		Task<ICacheLock> GetLockedLock(TKey key);

		/// <summary>
		/// Immediately breaks any existing lock
		/// </summary>
		/// <param name="key">Key of lock</param>
		/// <returns>Unlocked lock</returns>
		Task<ICacheLock> GetUnlockedLock(TKey key);

		/// <summary>
		/// Retrieves the current lock state
		/// </summary>
		/// <param name="key">Key of lock</param>
		/// <returns>State of lock</returns>
		Task<bool> HasLockedLock(TKey key);

		void RemoveLock(TKey key);
	}
}