using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
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

		public Task<AccountValidationTokenData> DeserializeRawToken(string token)
			=> Invoke(service => service.DeserializeRawToken(token));

		public Task<WorkerResponse<bool>> ValidateRegistration(AccountValidationTokenData tokenData)
			=> Invoke(service => service.ValidateRegistration(tokenData));
	}
}
