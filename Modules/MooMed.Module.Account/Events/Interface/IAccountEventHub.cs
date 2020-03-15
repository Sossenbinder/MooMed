using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Eventing.Events;

namespace MooMed.Module.Accounts.Events.Interface
{
    public interface IAccountEventHub
    {
	    [NotNull]
        MooEvent<ISessionContext> AccountLoggedIn { get; }

        [NotNull]
        MooEvent<ISessionContext> AccountLoggedOut { get; }
    }
}
