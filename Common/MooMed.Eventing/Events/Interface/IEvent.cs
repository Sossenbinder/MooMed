using System;
using System.Threading.Tasks;
using MooMed.DotNet.Utils.Disposable;

namespace MooMed.Eventing.Events.Interface
{
	public interface IEvent
	{
		DisposableAction Register(Func<Task> handler);

		void Unregister(Func<Task> handler);
	}

	/// <summary>
	/// Exposes methods for basic event registrations
	/// </summary>
	public interface IEvent<out TEventArgs>
	{
		DisposableAction Register(Func<TEventArgs, Task> handler);

		void Unregister(Func<TEventArgs, Task> handler);
	}
}