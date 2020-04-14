using JetBrains.Annotations;
using MooMed.Common.Definitions.Eventing.User;
using MooMed.Eventing.Events;
using MooMed.Eventing.Events.MassTransit.Interface;
using MooMed.Module.Accounts.Events.Interface;

namespace MooMed.Module.Accounts.Events
{
    public class AccountEventHub : IAccountEventHub
	{
		public MtMooEvent<AccountLoggedInEvent> AccountLoggedIn { get; }

		public MtMooEvent<AccountLoggedOutEvent> AccountLoggedOut { get; }

		public AccountEventHub([NotNull] IMassTransitEventingService massTransitEventingService)
	    {
			AccountLoggedIn = new MtMooEvent<AccountLoggedInEvent>("AccountLoggedIn", massTransitEventingService);
			AccountLoggedOut = new MtMooEvent<AccountLoggedOutEvent>("AccountLoggedOut", massTransitEventingService);
		}
    }
}
