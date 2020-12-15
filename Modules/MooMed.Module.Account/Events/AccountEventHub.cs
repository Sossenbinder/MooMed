using MooMed.Common.Definitions.Eventing.User;
using MooMed.Eventing.Events;
using MooMed.Eventing.Events.Interface;
using MooMed.Eventing.Events.MassTransit.Interface;
using MooMed.Logging.Abstractions.Interface;
using MooMed.Module.Accounts.Events.Interface;

namespace MooMed.Module.Accounts.Events
{
    public class AccountEventHub : IAccountEventHub
    {
        public IDistributedEvent<AccountLoggedInEvent> AccountLoggedIn { get; }

        public IDistributedEvent<AccountLoggedOutEvent> AccountLoggedOut { get; }

        public IDistributedEvent<AccountRegisteredEvent> AccountRegistered { get; }

        public AccountEventHub(
            IMassTransitEventingService massTransitEventingService,
            IMooMedLogger logger)
        {
            AccountLoggedIn = new MtMooEvent<AccountLoggedInEvent>(nameof(AccountLoggedIn), massTransitEventingService, logger);
            AccountLoggedOut = new MtMooEvent<AccountLoggedOutEvent>(nameof(AccountLoggedOut), massTransitEventingService, logger);
            AccountRegistered = new MtMooEvent<AccountRegisteredEvent>(nameof(AccountRegistered), massTransitEventingService, logger);
        }
    }
}