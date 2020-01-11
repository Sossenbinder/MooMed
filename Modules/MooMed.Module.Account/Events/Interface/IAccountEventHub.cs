using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;
using MooMed.Core.Code.Events;
using MooMed.Core.Translations;

namespace MooMed.Module.Accounts.Events.Interface
{
    public interface IAccountEventHub
    {
        [NotNull]
        MooEvent<(Account, Language)> AccountRegistered { get; }

        [NotNull]
        MooEvent<ISessionContext> AccountLoggedIn { get; }

        [NotNull]
        MooEvent<ISessionContext> AccountLoggedOut { get; }
    }
}
