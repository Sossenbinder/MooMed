using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Core.DataTypes;
using MooMed.IPC.EndpointResolution.Interface;
using MooMed.IPC.Grpc.Interface;
using MooMed.IPC.ProxyInvocation;
using MooMed.IPC.ProxyInvocation.Interface;

namespace MooMed.Stateful.SessionService.Remoting
{

	public class SessionServiceProxy : AbstractStatefulSetProxy<ISessionService>, ISessionService
	{
		public SessionServiceProxy(
			[NotNull] IEndpointProvider endpointProvider,
			[NotNull] IGrpcClientProvider grpcClientProvider,
			[NotNull] IDeterministicPartitionSelectorHelper deterministicPartitionSelectorHelper)
			: base(endpointProvider,
				grpcClientProvider,
				deterministicPartitionSelectorHelper,
				MooMedService.SessionService)
		{
		}

		public Task<ServiceResponse<ISessionContext>> GetSessionContext(Primitive<int> accountId)
			=> InvokeSpecificWithResult(accountId, service => service.GetSessionContext(accountId));

		public Task<ISessionContext> LoginAccount(Account account)
			=> InvokeSpecificWithResult(account.Id, service => service.LoginAccount(account));

		public Task UpdateSessionContext(ISessionContext sessionContext)
			=> InvokeSpecific(sessionContext, service => service.UpdateSessionContext(sessionContext));
	}
}
