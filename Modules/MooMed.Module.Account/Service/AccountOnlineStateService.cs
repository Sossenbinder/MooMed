using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MooMed.Common.Definitions.Eventing.User;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.Definitions.Notifications;
using MooMed.Eventing.Events.MassTransit.Interface;
using MooMed.Module.Accounts.Datatypes.Entity;
using MooMed.Module.Accounts.Datatypes.SignalR;
using MooMed.Module.Accounts.Events.Interface;
using MooMed.Module.Accounts.Repository.Interface;
using MooMed.Module.Accounts.Service.Interface;
using MooMed.SignalR.Hubs;

namespace MooMed.Module.Accounts.Service
{
	public class AccountOnlineStateService : IAccountOnlineStateService
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

			accountEventHub.AccountLoggedIn.Register(OnAccountLoggedIn);
			accountEventHub.AccountLoggedOut.Register(OnAccountLoggedOut);
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

			await _accountOnlineStateRepository.Update(sessionContext.Account.Id, entity => entity.OnlineState = AccountOnlineState.Offline);

			await SendFrontendNotification(loggedOutEvent.SessionContext, AccountOnlineState.Offline);
		}

		private async Task SendFrontendNotification([NotNull] ISessionContext sessionContext, AccountOnlineState onlineState)
		{
			var notification = new FrontendNotification<AccountOnlineStateFrontendNotification>()
			{
				Data = new AccountOnlineStateFrontendNotification()
				{
					AccountId = sessionContext.Account.Id,
					AccountOnlineState = onlineState,
				},
				NotificationType = NotificationType.FriendOnlineStateChange,
				Operation = Operation.Update,
			};

			var friendsOfAccount = await _friendsService.GetFriends(sessionContext);

			// TODO Try this with user
			await _massTransitSignalRBackplaneService.RaiseAllSignalREvent(notification);
		}
	}
}
