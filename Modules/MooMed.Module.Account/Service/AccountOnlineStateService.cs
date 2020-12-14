using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using MooMed.Common.Definitions.Eventing.User;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.Definitions.Notifications;
using MooMed.Common.ServiceBase.ServiceBase;
using MooMed.DotNet.Extensions;
using MooMed.Eventing.Events.MassTransit.Interface;
using MooMed.Eventing.Helper;
using MooMed.Module.Accounts.Datatypes.Entity;
using MooMed.Module.Accounts.Datatypes.SignalR;
using MooMed.Module.Accounts.Events.Interface;
using MooMed.Module.Accounts.Repository.Interface;
using MooMed.Module.Accounts.Service.Interface;

namespace MooMed.Module.Accounts.Service
{
    public class AccountOnlineStateService : MooMedServiceBase, IAccountOnlineStateService
    {
        [NotNull]
        private readonly IAccountOnlineStateRepository _accountOnlineStateRepository;

        [NotNull]
        private readonly IMassTransitSignalRBackplaneService _massTransitSignalRBackplaneService;

        [NotNull]
        private readonly IFriendsService _friendsService;

        public AccountOnlineStateService(
            [NotNull] IAccountEventHub accountEventHub,
            [NotNull] IAccountOnlineStateRepository accountOnlineStateRepository,
            [NotNull] IMassTransitSignalRBackplaneService massTransitSignalRBackplaneService,
            [NotNull] IFriendsService friendsService)
        {
            _accountOnlineStateRepository = accountOnlineStateRepository;
            _massTransitSignalRBackplaneService = massTransitSignalRBackplaneService;
            _friendsService = friendsService;

            RegisterEventHandler(accountEventHub.AccountLoggedIn, OnAccountLoggedIn);
            RegisterEventHandler(accountEventHub.AccountLoggedOut, OnAccountLoggedOut);
        }

        private async Task OnAccountLoggedIn(AccountLoggedInEvent loggedInEvent)
        {
            var sessionContext = loggedInEvent.SessionContext;

            var onlineStateEntity = new AccountOnlineStateEntity()
            {
                Id = sessionContext.Account.Id,
                OnlineState = AccountOnlineState.Online,
            };

            await _accountOnlineStateRepository.CreateOrUpdate(onlineStateEntity, entity => entity.OnlineState = AccountOnlineState.Online);

            await SendFrontendNotification(loggedInEvent.SessionContext, AccountOnlineState.Online);
        }

        private async Task OnAccountLoggedOut(AccountLoggedOutEvent loggedOutEvent)
        {
            var sessionContext = loggedOutEvent.SessionContext;

            await _accountOnlineStateRepository.Update(entity => entity.Id == sessionContext.Account.Id, entity => entity.OnlineState = AccountOnlineState.Offline);

            await SendFrontendNotification(loggedOutEvent.SessionContext, AccountOnlineState.Offline);
        }

        private async Task SendFrontendNotification([NotNull] ISessionContext sessionContext, AccountOnlineState onlineState)
        {
            var notification = FrontendNotificationFactory.Update(new AccountOnlineStateFrontendNotification()
            {
                AccountId = sessionContext.Account.Id,
                AccountOnlineState = onlineState,
            }, NotificationType.FriendOnlineStateChange);

            var friendsOfAccount = await _friendsService.GetFriends(sessionContext);
            var receiverIds = friendsOfAccount.Select(x => x.Id);

            await receiverIds.ParallelAsync(id => _massTransitSignalRBackplaneService.RaiseGroupSignalREvent(id.ToString(), notification));
        }

        public async Task<AccountOnlineState> GetOnlineStateForAccount(int accountId)
        {
            var onlineState = (await _accountOnlineStateRepository.Read(x => x.Id == accountId))
                .SingleOrDefault();

            return onlineState?.OnlineState ?? AccountOnlineState.Offline;
        }
    }
}