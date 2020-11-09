using System;
using System.Threading.Tasks;
using MooMed.Common.Definitions.IPC;
using MooMed.ServiceBase.Definitions.Interface;

namespace MooMed.RemotingProxies.ProxyInvocation.Interface
{
	public interface IStatefulSetProxy<out TServiceType> : IDeploymentProxy<TServiceType>
		where TServiceType : IGrpcService
	{
		/// <summary>
		/// Invoke a function on a specific replica
		/// </summary>
		/// <param name="endpointSelector">Selector for endpoint</param>
		/// <param name="invocationFunc">Function to invoke on the endpoint</param>
		/// <returns>Task representing the invocation</returns>
		Task InvokeSpecific(IEndpointSelector endpointSelector, Func<TServiceType, Task> invocationFunc);

		/// <summary>
		/// Invoke a function on a specific replica and return a result
		/// </summary>
		/// <param name="endpointSelector">Selector for endpoint</param>
		/// <param name="invocationFunc">Function to invoke on the endpoint</param>
		/// <returns>Task representing the invocation, containing the result</returns>
		Task<TResult> InvokeSpecificWithResult<TResult>(IEndpointSelector endpointSelector, Func<TServiceType, Task<TResult>> invocationFunc);
	}
}