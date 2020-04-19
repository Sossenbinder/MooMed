using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MooMed.Common.Definitions.Eventing.User;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.Definitions.Notifications;
using MooMed.Eventing.Events.MassTransit.Interface;
using MooMed.Module.Accounts.Datatypes.Entity;
using MooMed.Module.Accounts.Datatypes.SignalR;
using MooMed.Module.Accounts.Events.Interface;
using MooMed.Module.Accounts.Repository.Interface;
using MooMed.Module.Accounts.Service.Interface;

namespace MooMed.Module.Accounts.Service
{
	public class AccountOnlineStateService : IAccountOnlineStateService
	{
		[NotNull]
		private readonly IAccountOnlineStateRepository _accountOnlineStateRepository;

		[NotNull]
		private readonly IMassTransitEventingService _massTransitEventingService;

		public AccountOnlineStateService(
			[NotNull] IAccountEventHub accountEventHub,
			[NotNull] IAccountOnlineStateRepository accountOnlineStateRepository,
			[NotNull] IMassTransitEventingService massTransitEventingService)
		{
			_accountOnlineStateRepository = accountOnlineStateRepository;
			_massTransitEventingService = massTransitEventingService;

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

			await SendFrontendNotification(loggedInEvent.SessionContext.Account.Id, AccountOnlineState.Online);
		}

		private async Task OnAccountLoggedOut(AccountLoggedOutEvent loggedOutEvent)
		{
			var sessionContext = loggedOutEvent.SessionContext;

			await _accountOnlineStateRepository.Update(sessionContext.Account.Id, entity => entity.OnlineState = AccountOnlineState.Offline);

			await SendFrontendNotification(loggedOutEvent.SessionContext.Account.Id, AccountOnlineState.Offline);
		}

		private async Task SendFrontendNotification(int accountId, AccountOnlineState onlineState)
		{
			var notification = new FrontendNotification<AccountOnlineStateFrontendNotification>()
			{
				Data = new AccountOnlineStateFrontendNotification()
				{
					AccountId = accountId,
					AccountOnlineState = onlineState,
				},
				NotificationType = NotificationType.FriendOnlineStateChange,
				Operation = Operation.Update,
			};

			await _massTransitEventingService.RaiseSignalREvent(notification);
		}
	}
}
