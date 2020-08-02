using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.User;

namespace MooMed.Common.Definitions.Eventing.User
{
	public class AccountRegisteredEvent
	{
		[NotNull]
		public Account Account { get; }

		public AccountRegisteredEvent([NotNull] Account account)
		{
			Account = account;
		}
	}
}
