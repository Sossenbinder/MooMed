using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.User;

namespace MooMed.Common.Definitions.Models.Session.Interface
{
	public interface ISessionContext : IEndpointSelector
	{
		Account Account { get; set; }
	}
}