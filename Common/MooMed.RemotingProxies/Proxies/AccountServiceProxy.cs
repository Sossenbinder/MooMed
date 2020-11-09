using System.Collections.Generic;
using System.Threading.Tasks;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.Definitions.Models.User.ErrorCodes;
using MooMed.Core.DataTypes;
using MooMed.IPC.Grpc.Interface;
using MooMed.RemotingProxies.ProxyInvocation;
using MooMed.ServiceBase.Services.Interface;

namespace MooMed.RemotingProxies.Proxies
{
	public class AccountServiceProxy : AbstractDeploymentProxy<IAccountService>, IAccountService
	{
		public AccountServiceProxy(IGrpcClientProvider clientProvider)
			: base(clientProvider,
				DeploymentService.AccountService)
		{
		}

		public Task<ServiceResponse<LoginResult>> Login(LoginModel loginModel)
			=> InvokeWithResult(service => service.Login(loginModel));

		public Task RefreshLoginForAccount(Primitive<int> accountId)
			=> Invoke(service => service.RefreshLoginForAccount(accountId));

		public Task<ServiceResponse<RegistrationResult>> Register(RegisterModel registerModel)
			=> InvokeWithResult(service => service.Register(registerModel));

		public Task<ServiceResponse> LogOff(ISessionContext sessionContext)
			=> InvokeWithResult(service => service.LogOff(sessionContext));

		public Task<ServiceResponse<Account>> FindById(Primitive<int> accountId)
			=> InvokeWithResult(service => service.FindById(accountId));

		public Task<ServiceResponse<List<Account>>> FindAccountsStartingWithName(string name)
			=> InvokeWithResult(service => service.FindAccountsStartingWithName(name));

		public Task<ServiceResponse<Account>> FindByEmail(string email)
			=> InvokeWithResult(service => service.FindByEmail(email));

		public Task<ServiceResponse> AddAsFriend(AddAsFriendModel model)
			=> InvokeWithResult(service => service.AddAsFriend(model));

		public Task<ServiceResponse<List<Friend>>> GetFriends(ISessionContext sessionContext)
			=> InvokeWithResult(service => service.GetFriends(sessionContext));

		public Task<ServiceResponse<IdentityErrorCode>> UpdatePersonalData(PersonalData personalData)
			=> InvokeWithResult(service => service.UpdatePersonalData(personalData));

		public Task<ServiceResponse<IdentityErrorCode>> UpdatePassword(UpdatePassword updatePasswordData)
			=> InvokeWithResult(service => service.UpdatePassword(updatePasswordData));
	}
}