using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Caching.Helper;
using MooMed.Common.Definitions.Eventing.User;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.Session;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.ServiceBase;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Core.Code.Logging.Loggers.Interface;
using MooMed.Core.DataTypes;
using MooMed.Module.Accounts.Events.Interface;
using MooMed.Module.Session.Cache.Interface;

namespace MooMed.Stateful.SessionService.Service
{
    public class SessionService : MooMedServiceBase, ISessionService
    {
	    [NotNull]
	    private readonly ISessionContextCache _sessionContextCache;
        
	    public SessionService(
		    [NotNull] IMainLogger mainLogger,
		    [NotNull] ISessionContextCache sessionContextCache,
		    [NotNull] IAccountEventHub accountEventHub)
		    : base(mainLogger)
	    {
		    _sessionContextCache = sessionContextCache;

            accountEventHub.AccountLoggedOut.Register(OnAccountLoggedOut);
	    }

	    private void OnAccountLoggedOut([NotNull] AccountLoggedOutEvent accountLoggedOutEvent)
	    {
		    var sessionContext = accountLoggedOutEvent.SessionContext;

		    Logger.Debug($"AccountId {sessionContext.Account.Id} logged out.");
		    _sessionContextCache.RemoveItem(sessionContext);
	    }

	    [ItemCanBeNull]
        [CanBeNull]
        public Task<ServiceResponse<ISessionContext>> GetSessionContext(Primitive<int> accountId)
        {
            var accountIdKey = CacheKeyUtils.GetCacheKeyForAccountId(accountId);
            var sessionContext = _sessionContextCache.GetItem(accountIdKey);

            return Task.FromResult(sessionContext == null 
	            ? ServiceResponse<ISessionContext>.Failure() 
	            : ServiceResponse<ISessionContext>.Success(sessionContext));
        }

        [ItemNotNull]
        [NotNull]
        public Task<ISessionContext> LoginAccount(Account account)
        {
            var sessionContext = CreateSessionContext(account);

            _sessionContextCache.PutItem(sessionContext);

            return Task.FromResult<ISessionContext>(sessionContext);
        }

        public Task UpdateSessionContext(ISessionContext sessionContext)
        {
	        _sessionContextCache.PutItem(sessionContext);

	        return Task.CompletedTask;
        }

        [NotNull]
        private SessionContext CreateSessionContext([NotNull] Account account)
        {
            return new SessionContext()
            {
                Account = account
            };
        }
    }
}
