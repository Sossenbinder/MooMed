using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Grpc.Definitions.Interface;

namespace MooMed.IPC.ProxyInvocation.Interface
{
	public interface IProxy<out TServiceType> where TServiceType : IGrpcService
	{
		[NotNull]
		Task Invoke([NotNull] Func<TServiceType, Task> invocationFunc);

		[NotNull]
		Task<TResult> Invoke<TResult>([NotNull] Func<TServiceType, Task<TResult>> invocationFunc);

		[NotNull]
		Task Invoke([NotNull] IEndpointSelector endpointSelector, [NotNull] Func<TServiceType, Task> invocationFunc);

		[NotNull]
		Task<TResult> Invoke<TResult>([NotNull] IEndpointSelector endpointSelector, [NotNull] Func<TServiceType, Task<TResult>> invocationFunc);
	}
}
