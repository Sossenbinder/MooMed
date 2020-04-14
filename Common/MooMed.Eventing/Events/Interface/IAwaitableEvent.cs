using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace MooMed.Eventing.Events.Interface
{
	public interface IAwaitableEvent<TEventArgs>
	{
		Task<AccumulatedMooEventExceptions> Raise([NotNull] TEventArgs eventArgs);

		void Register([NotNull] Action<TEventArgs> handler);

		void Register([NotNull] Func<TEventArgs, Task> handler);

		void UnRegister([NotNull] Func<TEventArgs, Task> handler);
	}
}
