using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Eventing.Events.Interface;

[assembly:InternalsVisibleTo("MooMed.Core.Tests")]
namespace MooMed.Eventing.Events
{
    public class ServiceLocalMooEvent<TEventArgs> : IAwaitableEvent<TEventArgs>
    {
        [NotNull]
        private readonly List<Func<TEventArgs, Task>> _registeredActions;

        public ServiceLocalMooEvent()
        {
            _registeredActions = new List<Func<TEventArgs, Task>>();
        }

        [ItemNotNull]
        public async Task<AccumulatedMooEventExceptions> Raise(TEventArgs eventArgs)
        {
			var accumulatedExceptions = new AccumulatedMooEventExceptions();

            foreach (var registeredAction in _registeredActions)
            {
	            try
	            {
		            await registeredAction(eventArgs);
	            }
	            catch (Exception e)
	            {
		            accumulatedExceptions.Exceptions.Add(e);
	            }
            }

            return accumulatedExceptions;
        }

        public void Register(Action<TEventArgs> handler)
        {
			_registeredActions.Add(args =>
			{
				handler(args);

				return Task.CompletedTask;
			});
        }

		public void Register(Func<TEventArgs, Task> handler)
        {
            _registeredActions.Add(handler);
        }

        public void UnRegister(Func<TEventArgs, Task> handler)
        {
            _registeredActions.Remove(handler);
        }

        [NotNull]
        internal List<Func<TEventArgs, Task>> GetAllRegisteredEvents()
        {
            return _registeredActions;
        }
    }
}
