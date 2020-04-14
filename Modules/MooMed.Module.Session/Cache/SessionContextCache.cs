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
		private readonly ICache<ISessionContext> m_sessionContextCache;

		public SessionContextCache([NotNull] IDefaultCacheFactory defaultCacheFactory)
		{
			m_sessionContextCache = defaultCacheFactory.CreateCache<ISessionContext>();
		}

		public void PutItem(ISessionContext sessionContext, int? secondsToLive = null)
		{
			var key = sessionContext.GetAccountKey();
			m_sessionContextCache.PutItem(key, sessionContext, secondsToLive);
		}

		public ISessionContext GetItem(string key)
		{
			return m_sessionContextCache.GetItem(key);
		}

		public void RemoveItem(ISessionContext sessionContext)
		{
			m_sessionContextCache.Remove(sessionContext.GetAccountKey());
		}
	}
}
