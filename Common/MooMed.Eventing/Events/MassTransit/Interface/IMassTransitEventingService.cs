using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MassTransit;
using MooMed.Eventing.DataTypes;

namespace MooMed.Eventing.Events.MassTransit.Interface
{
    public interface IMassTransitEventingService
    {
        Task RaiseEvent<T>([NotNull] T message)
            where T : class;

        void RegisterForEvent<T>(string queueName, Action<T> handler, QueueType queueType = QueueType.RegularQueue)
            where T : class;

        void RegisterForEvent<T>([NotNull] string queueName, [NotNull] Func<T, Task> handler, QueueType queueType = QueueType.RegularQueue)
            where T : class;

        void RegisterConsumer<TConsumer>(string queueName, TConsumer consumer, QueueType queueType = QueueType.RegularQueue)
            where TConsumer : class, IConsumer;
    }
}