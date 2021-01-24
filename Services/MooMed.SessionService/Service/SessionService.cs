using JetBrains.Annotations;
using MooMed.Caching.Helper;
using MooMed.Common.Definitions.Eventing.User;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.Session;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.ServiceBase.ServiceBase;
using MooMed.Core.DataTypes;
using MooMed.Logging.Abstractions.Interface;
using MooMed.Module.Accounts.Events.Interface;
using MooMed.Module.Session.Cache.Interface;
using MooMed.ServiceBase.Services.Interface;
using System.Threading.Tasks;

namespace MooMed.SessionService.Service
{
	public class SessionService : ServiceBaseWithLogger, ISessionService
	{
		[NotNull]
		private readonly ISessionContextCache _sessionContextCache;

		public SessionService(
			[NotNull] IMooMedLogger logger,
			[NotNull] ISessionContextCache sessionContextCache,
			[NotNull] IAccountEventHub accountEventHub)
			: base(logger)
		{
			_sessionContextCache = sessionContextCache;

			RegisterEventHandler(accountEventHub.AccountLoggedOut, OnAccountLoggedOut);
		}

		private async Task OnAccountLoggedOut([NotNull] AccountLoggedOutEvent accountLoggedOutEvent)
		{
			var sessionContext = accountLoggedOutEvent.SessionContext;

			Logger.Debug($"AccountId {sessionContext.Account.Id} logged out.");
			await _sessionContextCache.RemoveItem(sessionContext);
		}

		public async Task<ServiceResponse<ISessionContext>> GetSessionContext(Primitive<int> accountId)
		{
			var key = CacheKeyUtils.GetCacheKeyForSessionContext(accountId);

			Logger.Info($"Key = {key}");
			var sessionContext = await _sessionContextCache.GetItem(key);

			Logger.Info($"Retrieved context for account {accountId} - {sessionContext == null}");

			return sessionContext == null
				? ServiceResponse<ISessionContext>.Failure()
				: ServiceResponse<ISessionContext>.Success(sessionContext);
		}

		public async Task<ISessionContext> LoginAccount(Account account)
		{
			var sessionContext = CreateSessionContext(account);
			Logger.Info($"Login of account {account.Id} with context {sessionContext}");

			await _sessionContextCache.PutItem(sessionContext);

			return sessionContext;
		}

		public async Task UpdateSessionContext(ISessionContext sessionContext)
		{
			await _sessionContextCache.PutItem(sessionContext);
		}

		private static SessionContext CreateSessionContext([NotNull] Account account)
		{
			return new()
			{
				Account = account
			};
		}
	}
}