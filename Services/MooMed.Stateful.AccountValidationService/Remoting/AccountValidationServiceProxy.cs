using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.Definitions.Models.User.ErrorCodes;
using MooMed.Core.DataTypes;
using MooMed.IPC.EndpointResolution.Interface;
using MooMed.IPC.Grpc.Interface;
using MooMed.IPC.ProxyInvocation;
using MooMed.IPC.ProxyInvocation.Interface;
using MooMed.ServiceBase.Services.Interface;

namespace MooMed.Stateful.AccountValidationService.Remoting
{
	public class AccountValidationServiceProxy : AbstractStatefulSetProxy<IAccountValidationService>, IAccountValidationService
	{
		public AccountValidationServiceProxy(
			[NotNull] IEndpointProvider endpointProvider,
			[NotNull] ISpecificGrpcClientProvider grpcClientProvider,
			[NotNull] IDeterministicPartitionSelectorHelper deterministicPartitionSelectorHelper)
			: base(endpointProvider,
				grpcClientProvider,
				deterministicPartitionSelectorHelper,
				StatefulSetService.AccountValidationService)
		{
		}

		public Task<ServiceResponse<IdentityErrorCode>> ValidateRegistration(AccountValidationModel accountValidationModel)
			=> InvokeRandomWithResult(service => service.ValidateRegistration(accountValidationModel));
	}
}