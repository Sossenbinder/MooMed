using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Core.DataTypes;
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
			[NotNull] IGrpcClientProvider grpcClientProvider,
			[NotNull] IDeterministicPartitionSelectorHelper deterministicPartitionSelectorHelper)
			: base(endpointProvider,
				grpcClientProvider,
				deterministicPartitionSelectorHelper,
				MooMedService.AccountValidationService)
		{
		}

		public Task SendAccountValidationMail(AccountValidationMailData accountValidationMailData)
			=> InvokeRandom(service => service.SendAccountValidationMail(accountValidationMailData));

		public Task<AccountValidationTokenData> DeserializeRawToken(string token)
			=> InvokeRandomWithResult(service => service.DeserializeRawToken(token));

		public Task<ServiceResponse<bool>> ValidateRegistration(AccountValidationTokenData tokenData)
			=> InvokeRandomWithResult(service => service.ValidateRegistration(tokenData));
	}
}
