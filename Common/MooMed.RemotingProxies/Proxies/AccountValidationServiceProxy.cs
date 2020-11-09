using System.Threading.Tasks;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.Definitions.Models.User.ErrorCodes;
using MooMed.Core.DataTypes;
using MooMed.IPC.Grpc.Interface;
using MooMed.RemotingProxies.ProxyInvocation;
using MooMed.ServiceBase.Services.Interface;

namespace MooMed.RemotingProxies.Proxies
{
	public class AccountValidationServiceProxy : AbstractDeploymentProxy<IAccountValidationService>, IAccountValidationService
	{
		public AccountValidationServiceProxy(IGrpcClientProvider clientProvider)
			: base(clientProvider,
				DeploymentService.AccountValidationService)
		{
		}

		public Task<ServiceResponse<IdentityErrorCode>> ValidateRegistration(AccountValidationModel accountValidationModel)
			=> InvokeWithResult(service => service.ValidateRegistration(accountValidationModel));
	}
}