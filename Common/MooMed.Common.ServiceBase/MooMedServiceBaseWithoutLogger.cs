using MassTransit;
using MooMed.DotNet.Extensions;
using MooMed.DotNet.Utils.Disposable;
using MooMed.Eventing.Events.Interface;
using System;
using System.Threading.Tasks;

namespace MooMed.Common.ServiceBase
{
	public class MooMedServiceBaseWithoutLogger : Disposable
	{
		protected void RegisterEventHandler<T>(
			IEvent<T> @event,
			Action<T> handler)
			=> RegisterEventHandler(@event, handler.MakeTaskCompatible()!);

		protected void RegisterEventHandler<T>(
			IEvent<T> @event,
			Func<T, Task> handler)
		{
			var disposeAction = @event.Register(handler);

			RegisterDisposable(disposeAction);
		}

		protected void RegisterFaultHandler<T>(
			IDistributedEvent<T> @event,
			Action<Fault<T>> handler)
			where T : class
			=> RegisterFaultHandler(@event, handler.MakeTaskCompatible()!);

		protected void RegisterFaultHandler<T>(
			IDistributedEvent<T> @event,
			Func<Fault<T>, Task> handler)
			where T : class
		{
			var disposeAction = @event.RegisterForErrors(handler);

			RegisterDisposable(disposeAction);
		}
	}
}