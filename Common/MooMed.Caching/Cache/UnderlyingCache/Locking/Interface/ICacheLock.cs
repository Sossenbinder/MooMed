using System.Threading.Tasks;

namespace MooMed.Caching.Cache.UnderlyingCache.Locking.Interface
{
	/// <summary>
	/// A lock to help restricting access to a cache item in order
	/// to avoid race conditions
	/// </summary>
	public interface ICacheLock
	{
		/// <summary>
		/// Locks the cache lock
		/// </summary>
		Task Lock();

		/// <summary>
		/// Unlocks the cache lock
		/// </summary>
		void Unlock();

		/// <summary>
		/// Checks whether the cache lock is locked
		/// </summary>
		/// <returns>Lock state</returns>
		bool IsLocked();
	}
}