using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MassTransit.SignalR.Utils;
using MooMed.DotNet.Utils.Disposable;
using MooMed.Eventing.Events.Interface;

namespace MooMed.Eventing.Events
{
    public abstract class EventBase<TEventArgs> : IEvent<TEventArgs>
    {
        protected readonly ConcurrentHashSet<Func<TEventArgs, Task>> Handlers = new();

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
            return Handlers.ToArray().ToList();
        }
    }
}