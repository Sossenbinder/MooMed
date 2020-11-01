using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.IPC.Grpc.Interface;
using MooMed.IPC.ProxyInvocation.Interface;
using MooMed.ServiceBase.Definitions.Interface;

namespace MooMed.IPC.ProxyInvocation
{
	/// <summary>
	/// Serves as the base for all proxy services, taking care of load balancing for all the implementers
	/// </summary>
	/// <typeparam name="TServiceType"></typeparam>
	public abstract class AbstractDeploymentProxy<TServiceType> : IDeploymentProxy<TServiceType>
		where TServiceType : class, IGrpcService
	{
		[NotNull]
		private readonly IGrpcClientProvider _clientProvider;

		private readonly DeploymentService _moomedService;

		protected AbstractDeploymentProxy(
			[NotNull] IGrpcClientProvider clientProvider,
			DeploymentService moomedService)
		{
			_clientProvider = clientProvider;

			_moomedService = moomedService;
		}

		public async Task Invoke(Func<TServiceType, Task> invocationFunc)
		{
			var proxy = await _clientProvider.GetGrpcClient<TServiceType>(_moomedService);

			await invocationFunc(proxy);
		}

		public async Task<TResult> InvokeWithResult<TResult>(Func<TServiceType, Task<TResult>> invocationFunc)
		{
			var proxy = await _clientProvider.GetGrpcClient<TServiceType>(_moomedService);

			return await invocationFunc(proxy);
		}
	}
}