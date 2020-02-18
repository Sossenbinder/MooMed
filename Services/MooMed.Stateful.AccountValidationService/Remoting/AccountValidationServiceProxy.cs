using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Core.DataTypes;
using MooMed.IPC.EndpointResolution.Interface;
using MooMed.IPC.ProxyInvocation;
using MooMed.IPC.ProxyInvocation.Interface;

namespace MooMed.Stateful.AccountValidationService.Remoting
{
	public class AccountValidationServiceProxy : AbstractProxy<IAccountValidationService>, IAccountValidationService
	{
		public AccountValidationServiceProxy(
			[NotNull] IStatefulCollectionInfoProvider statefulCollectionInfoProvider,
			[NotNull] IGrpcClientProvider grpcClientProvider,
			[NotNull] IDeterministicPartitionSelectorHelper deterministicPartitionSelectorHelper)
			: base(statefulCollectionInfoProvider,
				grpcClientProvider,
				deterministicPartitionSelectorHelper,
				DeployedService.AccountValidationService)
		{
		}

		public Task SendAccountValidationMail(AccountValidationMailData accountValidationMailData)
			=> InvokeOnRandomReplica(service => service.SendAccountValidationMail(accountValidationMailData));

		public Task<AccountValidationTokenData> DeserializeRawToken(string token)
			=> InvokeOnRandomReplica(service => service.DeserializeRawToken(token));

		public Task<ServiceResponse<bool>> ValidateRegistration(AccountValidationTokenData tokenData)
			=> InvokeOnRandomReplica(service => service.ValidateRegistration(tokenData));
	}
}
