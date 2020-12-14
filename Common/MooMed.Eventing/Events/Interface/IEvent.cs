using System;
using System.Threading.Tasks;
using MooMed.DotNet.Utils.Disposable;

namespace MooMed.Eventing.Events.Interface
{
    /// <summary>
    /// Exposes methods for basic event registrations
    /// </summary>
    public interface IEvent<out TEventArgs>
    {
        DisposableAction Register(Func<TEventArgs, Task> handler);

        void UnRegister(Func<TEventArgs, Task> handler);
    }
}