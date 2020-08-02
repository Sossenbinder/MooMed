using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.DotNet.Utils.Disposable;

namespace MooMed.Eventing.Events.Interface
{
	public interface IAwaitableEvent<TEventArgs>
	{
		Task<AccumulatedMooEventExceptions> Raise([NotNull] TEventArgs eventArgs);

		DisposableAction Register([NotNull] Action<TEventArgs> handler);

		DisposableAction Register([NotNull] Func<TEventArgs, Task> handler);

		void UnRegister([NotNull] Func<TEventArgs, Task> handler);
	}
}
