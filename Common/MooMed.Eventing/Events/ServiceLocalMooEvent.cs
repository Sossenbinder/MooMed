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
        private readonly List<Func<TEventArgs, Task>> m_registeredActions;

        public ServiceLocalMooEvent()
        {
            m_registeredActions = new List<Func<TEventArgs, Task>>();
        }

        [ItemNotNull]
        public async Task<AccumulatedMooEventExceptions> Raise([NotNull] TEventArgs eventArgs)
        {
			var accumulatedExceptions = new AccumulatedMooEventExceptions();

            foreach (var registeredAction in m_registeredActions)
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

        public void Register([NotNull] Action<TEventArgs> handler)
        {
			m_registeredActions.Add(args =>
			{
				handler(args);

				return Task.CompletedTask;
			});
        }

		public void Register([NotNull] Func<TEventArgs, Task> handler)
        {
            m_registeredActions.Add(handler);
        }

        public void UnRegister([NotNull] Func<TEventArgs, Task> handler)
        {
            m_registeredActions.Remove(handler);
        }

        [NotNull]
        internal List<Func<TEventArgs, Task>> GetAllRegisteredEvents()
        {
            return m_registeredActions;
        }
    }
}
