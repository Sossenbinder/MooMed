using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.User;
using MooMed.Core.DataTypes;
using MooMed.Grpc.Services.Interface;
using MooMed.IPC.EndpointResolution.Interface;
using MooMed.IPC.Grpc.Interface;
using MooMed.IPC.ProxyInvocation;
using MooMed.IPC.ProxyInvocation.Interface;

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

		public Task<ServiceResponse<bool>> ValidateRegistration(AccountValidationModel accountValidationModel)
			=> InvokeRandomWithResult(service => service.ValidateRegistration(accountValidationModel));
	}
}