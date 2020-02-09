using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Caching.Cache.CacheImplementations;
using MooMed.Caching.Cache.CacheImplementations.Interface;
using MooMed.Caching.Cache.Factory;
using MooMed.Caching.Cache.UnderlyingCache.Locking;
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

		public void PutItem(string key, ISessionContext value, int? secondsToLive = null)
		{
			m_sessionContextCache.PutItem(key, value, secondsToLive);
		}

		public ISessionContext GetItem(string key)
		{
			return m_sessionContextCache.GetItem(key);
		}
	}
}
