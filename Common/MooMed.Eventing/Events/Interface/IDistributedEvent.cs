using System;
using System.Threading.Tasks;
using MassTransit;
using MooMed.DotNet.Utils.Disposable;

namespace MooMed.Eventing.Events.Interface
{
    public interface IDistributedEvent<TEventArgs> : IEvent<TEventArgs>
        where TEventArgs : class
    {
        Task Raise(TEventArgs eventArgs);

        void RaiseFireAndForget(TEventArgs eventArgs);

        DisposableAction RegisterForErrors(Func<Fault<TEventArgs>, Task> faultHandler);
    }
}