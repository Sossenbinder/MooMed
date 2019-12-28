using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Caching.Cache.CacheImplementations.Interface;
using MooMed.Caching.Cache.Factory;
using MooMed.Common.Definitions.Models.Session;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.ServiceBase;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Core.Code.Logging.Loggers.Interface;

namespace MooMed.Stateful.SessionService.Service
{
    public class SessionService : MooMedServiceBase, ISessionService
    {
	    [NotNull]
	    private readonly ICache<ISessionContext> m_sessionContextCache;

	    public SessionService(
		    [NotNull] IMainLogger mainLogger,
		    [NotNull] IDefaultCacheFactory defaultCacheFactory)
		    : base(mainLogger)
	    {
		    m_sessionContextCache = defaultCacheFactory.CreateCache<ISessionContext>();
	    }

	    [ItemCanBeNull]
        [CanBeNull]
        public async Task<ISessionContext> GetSessionContext(int accountId)
        {
            var accountIdKey = GetKeyFromAccountId(accountId);
            var sessionContext = m_sessionContextCache.GetItem(accountIdKey);

            if (sessionContext == null)
            {
                return null;
            }

            m_sessionContextCache.PutItem(accountIdKey, sessionContext);
            return sessionContext;
        }

        [ItemNotNull]
        [NotNull]
        public async Task<ISessionContext> LoginAccount(Account account)
        {
            var sessionContext = CreateSessionContext(account);

            m_sessionContextCache.PutItem(account.IdAsKey(), sessionContext);

            return sessionContext;
        }

        [NotNull]
        private SessionContext CreateSessionContext([NotNull] Account account)
        {
            return new SessionContext()
            {
                Account = account
            };
        }

        [NotNull]
        private string GetKeyFromAccountId(int accountId)
        {
            return $"a-{accountId}";
        }
    }
}
