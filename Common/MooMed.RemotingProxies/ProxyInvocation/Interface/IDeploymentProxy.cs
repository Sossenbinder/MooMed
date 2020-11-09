using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.ServiceBase.Definitions.Interface;

namespace MooMed.RemotingProxies.ProxyInvocation.Interface
{
	public interface IDeploymentProxy<out TServiceType>
		where TServiceType : IGrpcService
	{
		/// <summary>
		/// Invoke a function on a random replica
		/// </summary>
		/// <param name="invocationFunc">Function to invoke on the endpoint</param>
		/// <returns>Task representing the invocation</returns>
		[NotNull]
		Task Invoke([NotNull] Func<TServiceType, Task> invocationFunc);

		/// <summary>
		/// Invoke a function on a random replica and return a result
		/// </summary>
		/// <param name="invocationFunc">Function to invoke on the endpoint</param>
		/// <returns>Task representing the invocation, containing the result</returns>
		[NotNull]
		Task<TResult> InvokeWithResult<TResult>([NotNull] Func<TServiceType, Task<TResult>> invocationFunc);
	}
}