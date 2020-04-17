using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MooMed.Common.Definitions.Eventing.User;
using MooMed.Common.Definitions.Models.User;
using MooMed.Module.Accounts.Datatypes.Entity;
using MooMed.Module.Accounts.Events.Interface;
using MooMed.Module.Accounts.Repository.Interface;
using MooMed.Module.Accounts.Service.Interface;

namespace MooMed.Module.Accounts.Service
{
	public class AccountOnlineStateService : IAccountOnlineStateService
	{
		[NotNull]
		private readonly IAccountOnlineStateRepository _accountOnlineStateRepository;

		public AccountOnlineStateService(
			[NotNull] IAccountEventHub accountEventHub,
			[NotNull] IAccountOnlineStateRepository accountOnlineStateRepository)
		{
			_accountOnlineStateRepository = accountOnlineStateRepository;

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
		}

		private async Task OnAccountLoggedOut(AccountLoggedOutEvent loggedOutEvent)
		{
			var sessionContext = loggedOutEvent.SessionContext;

			await _accountOnlineStateRepository.Update(sessionContext.Account.Id, entity => entity.OnlineState = AccountOnlineState.Offline);
		}
	}
}
