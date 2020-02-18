using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Core.DataTypes;
using MooMed.Core.Translations;
using MooMed.IPC.EndpointResolution.Interface;
using MooMed.IPC.ProxyInvocation;
using MooMed.IPC.ProxyInvocation.Interface;
using ProtoBuf.Grpc;

namespace MooMed.Stateful.AccountService.Remoting
{
	public class AccountServiceProxy : AbstractProxy<IAccountService>, IAccountService
	{
		public AccountServiceProxy(
			[NotNull] IStatefulCollectionInfoProvider statefulCollectionInfoProvider,
			[NotNull] IGrpcClientProvider grpcClientProvider,
			[NotNull] IDeterministicPartitionSelectorHelper deterministicPartitionSelectorHelper)
			: base(statefulCollectionInfoProvider,
				grpcClientProvider,
				deterministicPartitionSelectorHelper,
				DeployedService.AccountService)
		{
		}

		public Task<ServiceResponse<LoginResult>> Login(LoginModel loginModel)
			=> InvokeOnRandomReplica(service => service.Login(loginModel));

		public Task RefreshLoginForAccount(Primitive<int> accountId)
			=> InvokeOnRandomReplica(service => service.RefreshLoginForAccount(accountId));

		public Task<RegistrationResult> Register(RegisterModel registerModel)
			=> InvokeOnRandomReplica(service => service.Register(registerModel));
		
		public Task LogOff(ISessionContext sessionContext)
			=> Invoke(sessionContext, service => service.LogOff(sessionContext));

		public Task<Account> FindById(Primitive<int> accountId)
			=> InvokeOnRandomReplica(service => service.FindById(accountId));

		public Task<List<Account>> FindAccountsStartingWithName(string name)
			=> InvokeOnRandomReplica(service => service.FindAccountsStartingWithName(name));

		public Task<Account> FindByEmail(string email)
			=> InvokeOnRandomReplica(service => service.FindByEmail(email));
	}
}
