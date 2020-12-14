﻿using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Caching.Helper;
using MooMed.Common.Definitions.Eventing.User;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Logging;
using MooMed.Common.Definitions.Models.Session;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.ServiceBase.ServiceBase;
using MooMed.Core.DataTypes;
using MooMed.Module.Accounts.Events.Interface;
using MooMed.Module.Session.Cache.Interface;
using MooMed.ServiceBase.Services.Interface;

namespace MooMed.SessionService.Service
{
    public class SessionService : MooMedServiceBaseWithLogger, ISessionService
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

        private void OnAccountLoggedOut([NotNull] AccountLoggedOutEvent accountLoggedOutEvent)
        {
            var sessionContext = accountLoggedOutEvent.SessionContext;

            Logger.Debug($"AccountId {sessionContext.Account.Id} logged out.");
            _sessionContextCache.RemoveItem(sessionContext);
        }

        public async Task<ServiceResponse<ISessionContext>> GetSessionContext(Primitive<int> accountId)
        {
            var key = CacheKeyUtils.GetCacheKeyForSessionContext(accountId);
            var sessionContext = await _sessionContextCache.GetItem(key);

            return sessionContext == null
                ? ServiceResponse<ISessionContext>.Failure()
                : ServiceResponse<ISessionContext>.Success(sessionContext);
        }

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

        private static SessionContext CreateSessionContext([NotNull] Account account)
        {
            return new()
            {
                Account = account
            };
        }
    }
}