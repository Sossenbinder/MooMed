using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.Session;
using MooMed.Common.Definitions.Models.User;
using MooMed.ServiceBase.Service.Interface.Base;

namespace MooMed.ServiceBase.Service.Interface
{
	public interface ISessionServiceProxy : ISessionService
	{

	}

    public interface ISessionService : IRemotingServiceBase
	{ 
		[NotNull]
        Task<SessionContext> GetSessionContext(int accountId);

		[NotNull]
        Task<SessionContext> LoginAccount([NotNull] Account account);
    }
}
 