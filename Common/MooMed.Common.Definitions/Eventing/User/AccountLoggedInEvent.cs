using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.Session.Interface;

namespace MooMed.Common.Definitions.Eventing.User
{
	public class AccountLoggedInEvent : IUserEvent
	{
		[NotNull]
		public ISessionContext SessionContext { get; }

		public AccountLoggedInEvent([NotNull] ISessionContext sessionContext)
		{
			SessionContext = sessionContext;
		}
	}
}
