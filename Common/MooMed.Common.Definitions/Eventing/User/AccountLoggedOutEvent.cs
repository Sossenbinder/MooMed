using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.Session.Interface;

namespace MooMed.Common.Definitions.Eventing.User
{
	public class AccountLoggedOutEvent : IUserEvent
	{
		[NotNull]
		public ISessionContext SessionContext { get; }

		public AccountLoggedOutEvent([NotNull] ISessionContext sessionContext)
		{
			SessionContext = sessionContext;
		}
	}
}
