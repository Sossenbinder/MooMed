using JetBrains.Annotations;
using MooMed.Caching.Cache.CacheImplementations.Interface;
using MooMed.Caching.Cache.Factory;
using MooMed.Caching.Extensions;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Module.Session.Cache.Interface;

namespace MooMed.Module.Session.Cache
{
	public class SessionContextCache : ISessionContextCache
	{
		[NotNull]
		private readonly ICache<ISessionContext> _sessionContextCache;

		public SessionContextCache([NotNull] IDefaultCacheFactory defaultCacheFactory)
		{
			_sessionContextCache = defaultCacheFactory.CreateCache<ISessionContext>();
		}

		public void PutItem(ISessionContext sessionContext, int? secondsToLive = null)
		{
			var key = sessionContext.GetAccountKey();
			_sessionContextCache.PutItem(key, sessionContext, secondsToLive);
		}

		public ISessionContext GetItem(string key)
		{
			return _sessionContextCache.GetItem(key);
		}

		public void RemoveItem(ISessionContext sessionContext)
		{
			_sessionContextCache.Remove(sessionContext.GetAccountKey());
		}
	}
}
