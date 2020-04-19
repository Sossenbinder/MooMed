using MooMed.Common.Definitions.Models.User;

namespace MooMed.Module.Accounts.Datatypes.SignalR
{
	public class AccountOnlineStateFrontendNotification
	{
		public int AccountId { get; set; }

		public AccountOnlineState AccountOnlineState { get; set; }
	}
}
