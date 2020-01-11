using System.Runtime.Serialization;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Caching.Cache.CacheImplementations.Interface;
using MooMed.Caching.Cache.Factory;
using MooMed.Common.Definitions.Models.Session;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.ServiceBase.Interface;

namespace MooMed.Module.Session.Service
{
    [KnownType(typeof(SessionContext))]
    public class SessionService : ISessionService
    {
        [NotNull]
        private readonly ICache<ISessionContext> m_sessionContextCache;

        public SessionService([NotNull] IDefaultCacheFactory cacheFactory)
        {
	        m_sessionContextCache = cacheFactory.CreateCache<ISessionContext>();

        }

        [ItemCanBeNull]
        [CanBeNull]
        public async Task<ISessionContext> GetSessionContext(AccountIdQuery accountIdQuery)
        {
            var accountIdKey = GetKeyFromAccountId(accountIdQuery.AccountId);
            var sessionContext = m_sessionContextCache.GetItem(accountIdKey);

            if (sessionContext == null)
            {
                return null;
            }

            m_sessionContextCache.PutItem(accountIdKey, sessionContext);
            return (SessionContext)sessionContext;
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
        private ISessionContext CreateSessionContext([NotNull] Account account)
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