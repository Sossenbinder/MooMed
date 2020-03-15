using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Eventing.Events;
using MooMed.Module.Accounts.Events.Interface;

namespace MooMed.Module.Accounts.Events
{
    public class AccountEventHub : IAccountEventHub
    {
	    public MooEvent<ISessionContext> AccountLoggedIn { get; } = new MooEvent<ISessionContext>();

        public MooEvent<ISessionContext> AccountLoggedOut { get; } = new MooEvent<ISessionContext>();
    }
}
