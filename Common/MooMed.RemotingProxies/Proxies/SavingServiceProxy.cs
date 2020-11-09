using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.Saving;
using MooMed.Core.DataTypes;
using MooMed.IPC.Grpc.Interface;
using MooMed.RemotingProxies.ProxyInvocation;
using MooMed.ServiceBase.Services.Interface;

namespace MooMed.RemotingProxies.Proxies
{
	public class SavingServiceProxy : AbstractDeploymentProxy<ISavingService>, ISavingService
	{
		public SavingServiceProxy([NotNull] IGrpcClientProvider clientProvider)
			: base(clientProvider, DeploymentService.SavingService)
		{
		}

		public Task<ServiceResponse> SetCurrency(SetCurrencyModel setCurrencyModel)
			=> InvokeWithResult(service => service.SetCurrency(setCurrencyModel));
	}
}