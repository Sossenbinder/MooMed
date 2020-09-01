using System;
using System.Threading.Tasks;
using MooMed.Common.ServiceBase.Utils;
using MooMed.Eventing.Events.Interface;

namespace MooMed.Common.ServiceBase.ServiceBase
{
	public class MooMedServiceBase : Disposable
	{
		protected void RegisterEventHandler<T>(
			IAwaitableEvent<T> awaitableEvent,
			Action<T> handler)
		{
			var disposeAction = awaitableEvent.Register(handler);

			RegisterDisposable(disposeAction);
		}

		protected void RegisterEventHandler<T>(
			IAwaitableEvent<T> awaitableEvent,
			Func<T, Task> handler)
		{
			var disposeAction = awaitableEvent.Register(handler);

			RegisterDisposable(disposeAction);
		}
	}
}