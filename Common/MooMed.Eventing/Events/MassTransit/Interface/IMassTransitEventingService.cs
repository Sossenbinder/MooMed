using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MassTransit;

namespace MooMed.Eventing.Events.MassTransit.Interface
{
	public interface IMassTransitEventingService
	{
		Task RaiseEvent<T>([NotNull] T message)
			where T : class;

		void RegisterForEvent<T>([NotNull] string queueName, [NotNull] Func<ConsumeContext<T>, Task> handler)
			where T : class;

		void RegisterForEvent<T>([NotNull] string queueName, [NotNull] Action<T> handler)
			where T : class;

		void RegisterForEvent<T>([NotNull] string queueName, [NotNull] Func<T, Task> handler)
			where T : class;
	}
}
