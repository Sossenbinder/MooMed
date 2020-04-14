using MooMed.Common.Definitions.Models.Session.Interface;

namespace MooMed.Common.Definitions.Eventing.User
{
	public interface IUserEvent
	{
		public ISessionContext SessionContext { get; }
	}
}
