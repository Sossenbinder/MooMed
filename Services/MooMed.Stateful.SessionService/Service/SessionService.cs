using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Caching.Cache.CacheImplementations.Interface;
using MooMed.Caching.Cache.Factory;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.Session;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.ServiceBase;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Core.Code.Logging.Loggers.Interface;
using MooMed.Core.DataTypes;
using MooMed.Module.Session.Cache.Interface;

namespace MooMed.Stateful.SessionService.Service
{
    public class SessionService : MooMedServiceBase, ISessionService
    {
	    [NotNull]
	    private readonly ISessionContextCache m_sessionContextCache;

	    public SessionService(
		    [NotNull] IMainLogger mainLogger,
		    [NotNull] ISessionContextCache sessionContextCache)
		    : base(mainLogger)
	    {
		    m_sessionContextCache = sessionContextCache;
	    }

	    [ItemCanBeNull]
        [CanBeNull]
        public Task<ServiceResponse<ISessionContext>> GetSessionContext(Primitive<int> accountId)
        {
            var accountIdKey = GetKeyFromAccountId(accountId);
            var sessionContext = m_sessionContextCache.GetItem(accountIdKey);

            if (sessionContext == null)
            {
                return Task.FromResult(ServiceResponse<ISessionContext>.Failure());
            }

            return Task.FromResult(ServiceResponse<ISessionContext>.Success(sessionContext));
        }

        [ItemNotNull]
        [NotNull]
        public Task<ISessionContext> LoginAccount(Account account)
        {
            var sessionContext = CreateSessionContext(account);

            m_sessionContextCache.PutItem(account.IdAsKey(), sessionContext);

            return Task.FromResult<ISessionContext>(sessionContext);
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
