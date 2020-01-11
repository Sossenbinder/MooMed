using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;
using MooMed.Core.Code.Events;
using MooMed.Core.Translations;
using MooMed.Module.Accounts.Events.Interface;

namespace MooMed.Module.Accounts.Events
{
    public class AccountEventHub : IAccountEventHub
    {
        public MooEvent<(Account, Language)> AccountRegistered { get; } = new MooEvent<(Account, Language)>();

        public MooEvent<ISessionContext> AccountLoggedIn { get; } = new MooEvent<ISessionContext>();

        public MooEvent<ISessionContext> AccountLoggedOut { get; } = new MooEvent<ISessionContext>();
    }
}
