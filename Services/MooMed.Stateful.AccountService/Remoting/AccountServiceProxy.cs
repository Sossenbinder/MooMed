using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Core.DataTypes;
using MooMed.Core.Translations;
using MooMed.IPC.EndpointResolution.Interface;
using MooMed.IPC.ProxyInvocation;
using MooMed.IPC.ProxyInvocation.Interface;

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

		public Task<WorkerResponse<LoginResult>> Login(LoginModel loginModel)
			=> Invoke(service => service.Login(loginModel));

		public Task RefreshLoginForAccount(int accountId)
			=> Invoke(service => service.RefreshLoginForAccount(accountId));

		public Task<RegistrationResult> Register(RegisterModel registerModel, Language lang)
			=> Invoke(service => service.Register(registerModel, lang));
		
		public Task LogOff(ISessionContext sessionContext)
			=> Invoke(sessionContext, service => service.LogOff(sessionContext));

		public Task<Account> FindById(int accountId)
			=> Invoke(service => service.FindById(accountId));

		public Task<List<Account>> FindAccountsStartingWithName(string name)
			=> Invoke(service => service.FindAccountsStartingWithName(name));

		public Task<Account> FindByEmail(string email)
			=> Invoke(service => service.FindByEmail(email));
	}
}
