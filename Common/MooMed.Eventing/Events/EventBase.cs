using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.DotNet.Utils.Disposable;
using MooMed.Eventing.Events.Interface;

namespace MooMed.Eventing.Events
{
	public abstract class EventBase<TEventArgs> : IAwaitableEvent<TEventArgs>
	{
		[NotNull]
		protected readonly List<Func<TEventArgs, Task>> Handlers;

		protected EventBase()
		{
			Handlers = new List<Func<TEventArgs, Task>>();
		}

		public abstract Task<AccumulatedMooEventExceptions> Raise(TEventArgs eventArgs);

		public DisposableAction Register(Action<TEventArgs> handler)
		{
			return Register(eventArgs =>
			{
				handler(eventArgs);
				return Task.CompletedTask;
			});
		}

		public DisposableAction Register(Func<TEventArgs, Task> handler)
		{
			Handlers.Add(handler);

			return new DisposableAction(() => UnRegister(handler));
		}

		public void UnRegister(Func<TEventArgs, Task> handler)
		{
			Handlers.Remove(handler);
		}

		[NotNull]
		internal List<Func<TEventArgs, Task>> GetAllRegisteredEvents()
		{
			return Handlers;
		}
	}
}
