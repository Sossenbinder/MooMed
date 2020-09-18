using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.Definitions.Models.User.ErrorCodes;
using MooMed.Core.DataTypes;
using MooMed.IPC.EndpointResolution.Interface;
using MooMed.IPC.Grpc.Interface;
using MooMed.IPC.ProxyInvocation;
using MooMed.IPC.ProxyInvocation.Interface;
using MooMed.ServiceBase.Services.Interface;

namespace MooMed.Stateful.AccountService.Remoting
{
	public class AccountServiceProxy : AbstractStatefulSetProxy<IAccountService>, IAccountService
	{
		public AccountServiceProxy(
			[NotNull] IEndpointProvider endpointProvider,
			[NotNull] ISpecificGrpcClientProvider grpcClientProvider,
			[NotNull] IDeterministicPartitionSelectorHelper deterministicPartitionSelectorHelper)
			: base(endpointProvider,
				grpcClientProvider,
				deterministicPartitionSelectorHelper,
				StatefulSetService.AccountService)
		{
		}

		public Task<ServiceResponse<LoginResult>> Login(LoginModel loginModel)
			=> InvokeRandomWithResult(service => service.Login(loginModel));

		public Task RefreshLoginForAccount(Primitive<int> accountId)
			=> InvokeRandom(service => service.RefreshLoginForAccount(accountId));

		public Task<ServiceResponse<RegistrationResult>> Register(RegisterModel registerModel)
			=> InvokeRandomWithResult(service => service.Register(registerModel));

		public Task<ServiceResponse> LogOff(ISessionContext sessionContext)
			=> InvokeSpecificWithResult(sessionContext, service => service.LogOff(sessionContext));

		public Task<ServiceResponse<Account>> FindById(Primitive<int> accountId)
			=> InvokeRandomWithResult(service => service.FindById(accountId));

		public Task<ServiceResponse<List<Account>>> FindAccountsStartingWithName(string name)
			=> InvokeRandomWithResult(service => service.FindAccountsStartingWithName(name));

		public Task<ServiceResponse<Account>> FindByEmail(string email)
			=> InvokeRandomWithResult(service => service.FindByEmail(email));

		public Task<ServiceResponse> AddAsFriend(AddAsFriendModel model)
			=> InvokeSpecificWithResult(model, service => service.AddAsFriend(model));

		public Task<ServiceResponse<List<Friend>>> GetFriends(ISessionContext sessionContext)
			=> InvokeSpecificWithResult(sessionContext, service => service.GetFriends(sessionContext));

		public Task<ServiceResponse<IdentityErrorCode>> UpdatePersonalData(PersonalData personalData)
			=> InvokeSpecificWithResult(personalData, service => service.UpdatePersonalData(personalData));

		public Task<ServiceResponse<IdentityErrorCode>> UpdatePassword(UpdatePassword updatePasswordData)
			=> InvokeSpecificWithResult(updatePasswordData, service => service.UpdatePassword(updatePasswordData));
	}
}