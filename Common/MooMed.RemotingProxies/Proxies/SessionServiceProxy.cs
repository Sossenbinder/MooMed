using System.Threading.Tasks;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;
using MooMed.Core.DataTypes;
using MooMed.IPC.Grpc.Interface;
using MooMed.RemotingProxies.ProxyInvocation;
using MooMed.ServiceBase.Services.Interface;

namespace MooMed.RemotingProxies.Proxies
{
	public class SessionServiceProxy : AbstractDeploymentProxy<ISessionService>, ISessionService
	{
		public SessionServiceProxy(IGrpcClientProvider clientProvider)
			: base(clientProvider,
				DeploymentService.SessionService)
		{
		}

		public Task<ServiceResponse<ISessionContext>> GetSessionContext(Primitive<int> accountId)
			=> InvokeWithResult(service => service.GetSessionContext(accountId));

		public Task<ISessionContext> LoginAccount(Account account)
			=> InvokeWithResult(service => service.LoginAccount(account));

		public Task UpdateSessionContext(ISessionContext sessionContext)
			=> Invoke(service => service.UpdateSessionContext(sessionContext));
	}
}