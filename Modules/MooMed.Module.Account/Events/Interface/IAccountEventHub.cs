using JetBrains.Annotations;
using MooMed.Common.Definitions.Eventing.User;
using MooMed.Eventing.Events.Interface;

namespace MooMed.Module.Accounts.Events.Interface
{
    public interface IAccountEventHub
    {
        [NotNull]
        IDistributedEvent<AccountLoggedInEvent> AccountLoggedIn { get; }

        [NotNull]
        IDistributedEvent<AccountLoggedOutEvent> AccountLoggedOut { get; }

        [NotNull]
        IDistributedEvent<AccountRegisteredEvent> AccountRegistered { get; }
    }
}