using System.Threading.Tasks;
using MooMed.Caching.Cache.CacheImplementations.Interface;
using MooMed.Caching.Cache.Factory.Interface;
using MooMed.Caching.Extensions;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Module.Session.Cache.Interface;

namespace MooMed.Module.Session.Cache
{
	public class SessionContextCache : ISessionContextCache
	{
		private readonly ICache<ISessionContext> _sessionContextCache;

		public SessionContextCache(IDistributedCacheFactory cacheFactory)
		{
			_sessionContextCache = cacheFactory.CreateCache<ISessionContext>();
		}

		public ValueTask PutItem(ISessionContext sessionContext, int? secondsToLive = null)
		{
			var key = sessionContext.GetAccountKey();
			return _sessionContextCache.PutItem(key, sessionContext, secondsToLive);
		}

		public ValueTask<ISessionContext> GetItem(string key)
		{
			return _sessionContextCache.GetItem(key);
		}

		public ValueTask RemoveItem(ISessionContext sessionContext)
		{
			return _sessionContextCache.Remove(sessionContext.GetAccountKey());
		}
	}
}