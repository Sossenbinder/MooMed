using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.ServiceBase.Service.Interface.Base;

namespace MooMed.ServiceRemoting.ProxyInvocation.Interface
{
	public interface IPartitioningProxyInvoker<out TServiceType> where TServiceType : IRemotingServiceBase
	{
		Task Invoke([NotNull] Func<TServiceType, Task> invocationFunc);

		Task<TResult> Invoke<TResult>([NotNull] Func<TServiceType, Task<TResult>> invocationFunc);

		Task Invoke([NotNull] IPartitionSelector sessionContext, [NotNull] Func<TServiceType, Task> invocationFunc);

		Task<TResult> Invoke<TResult>([NotNull] IPartitionSelector sessionContext, [NotNull] Func<TServiceType, Task<TResult>> invocationFunc);
	}
}
