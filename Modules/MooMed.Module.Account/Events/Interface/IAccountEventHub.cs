using JetBrains.Annotations;
using MooMed.Common.Definitions.Eventing.User;
using MooMed.Eventing.Events;

namespace MooMed.Module.Accounts.Events.Interface
{
    public interface IAccountEventHub
    {
	    [NotNull]
	    MtMooEvent<AccountLoggedInEvent> AccountLoggedIn { get; }

        [NotNull]
        MtMooEvent<AccountLoggedOutEvent> AccountLoggedOut { get; }

        [NotNull] 
        MtMooEvent<AccountRegisteredEvent> AccountRegistered { get; }
    }
}
