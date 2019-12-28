using System.Threading.Tasks;

namespace MooMed.Caching.Cache.UnderlyingCache.Locking.Interface
{
	public interface ICacheLock
	{
		Task Lock();

		void Unlock();

		bool IsLocked();
	}
}
