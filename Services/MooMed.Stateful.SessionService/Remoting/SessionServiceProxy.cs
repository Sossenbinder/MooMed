using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Core.DataTypes;
using MooMed.IPC.EndpointResolution.Interface;
using MooMed.IPC.ProxyInvocation;
using MooMed.IPC.ProxyInvocation.Interface;

namespace MooMed.Stateful.SessionService.Remoting
{

	public class SessionServiceProxy : AbstractProxy<ISessionService>, ISessionService
	{
		public SessionServiceProxy(
			[NotNull] IStatefulCollectionInfoProvider statefulCollectionInfoProvider,
			[NotNull] IGrpcClientProvider grpcClientProvider,
			[NotNull] IDeterministicPartitionSelectorHelper deterministicPartitionSelectorHelper)
			: base(statefulCollectionInfoProvider,
				grpcClientProvider,
				deterministicPartitionSelectorHelper,
				StatefulSet.SessionService)
		{
		}

		public Task<ServiceResponse<ISessionContext>> GetSessionContext(Primitive<int> accountId)
			=> Invoke(accountId, service => service.GetSessionContext(accountId));

		public Task<ISessionContext> LoginAccount(Account account)
			=> Invoke(account.Id, service => service.LoginAccount(account));

		public Task UpdateSessionContext(ISessionContext sessionContext)
			=> Invoke(sessionContext, service => service.UpdateSessionContext(sessionContext));
	}
}
