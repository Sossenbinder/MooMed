using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.SignalR.Utils;
using MooMed.DotNet.Extensions;
using MooMed.DotNet.Utils.Disposable;
using MooMed.DotNet.Utils.Tasks;
using MooMed.Eventing.DataTypes;
using MooMed.Eventing.Events.Interface;
using MooMed.Eventing.Events.MassTransit.Interface;
using MooMed.Logging.Abstractions.Interface;

namespace MooMed.Eventing.Events
{
    /// <summary>
    /// Event hub which registers itself as IConsumer and then distributes events based on service-local registrations.
    /// This way, there is a more fine-grained control for disposal and lifetimes, as MT does not support
    /// connecting / disconnecting on the fly
    /// </summary>
    /// <typeparam name="TEventArgs"></typeparam>
    public class MtMooEvent<TEventArgs> : EventBase<TEventArgs>, IDistributedEvent<TEventArgs>, IConsumer<TEventArgs>
        where TEventArgs : class
    {
        private readonly string _queueName;

        private readonly IMassTransitEventingService _massTransitEventingService;

        private readonly IMooMedLogger _logger;

        private readonly ConcurrentHashSet<Func<Fault<TEventArgs>, Task>> _faultHandlers = new();

        private readonly object _registrationLock = new();

        public MtMooEvent(
            string queueName,
            IMassTransitEventingService massTransitEventingService,
            IMooMedLogger logger)
        {
            _queueName = queueName;
            _massTransitEventingService = massTransitEventingService;
            _logger = logger;

            _massTransitEventingService.RegisterConsumer(queueName, this);
        }

        public async Task Consume(ConsumeContext<TEventArgs> context)
        {
            try
            {
                var message = context.Message;
                await Handlers.ToArray().ParallelAsync(handler => handler(message));
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);

                // It is important to throw here, as a consumer registration will raise an {queueName}_error event
                // when an exception is passed to the calling MassTransit code!
                throw;
            }
        }

        public void RaiseFireAndForget(TEventArgs eventArgs)
        {
            _ = FireAndForgetTask.Run(() => _massTransitEventingService.RaiseEvent(eventArgs), _logger);
        }

        public DisposableAction RegisterForErrors(Func<Fault<TEventArgs>, Task> faultHandler)
        {
            _faultHandlers.Add(faultHandler);

            lock (_registrationLock)
            {
                _massTransitEventingService.RegisterForEvent<Fault<TEventArgs>>($"{_queueName}", OnErrorReceived, QueueType.ErrorQueue);
            }

            return new DisposableAction(() => _faultHandlers.Remove(faultHandler));
        }

        public Task Raise(TEventArgs eventArgs)
        {
            return _massTransitEventingService.RaiseEvent(eventArgs);
        }

        private async Task OnErrorReceived(Fault<TEventArgs> eventArgs)
        {
            try
            {
                await _faultHandlers.ToArray().ParallelAsync(handler => handler(eventArgs));
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
            }
        }
    }
}