using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JetBrains.Annotations;

[assembly:InternalsVisibleTo("MooMed.Core.Tests")]

namespace MooMed.Core.Code.Events
{
    public class MooEvent<TEventArgs>
    {
        [NotNull]
        private readonly List<Func<TEventArgs, Task>> m_registeredActions;

        public MooEvent()
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

        public void Register([NotNull] Action<TEventArgs> actionItem)
        {
			m_registeredActions.Add(args =>
			{
				actionItem(args);

				return Task.CompletedTask;
			});
        }

		public void Register([NotNull] Func<TEventArgs, Task> actionItem)
        {
            m_registeredActions.Add(actionItem);
        }

        public void UnRegister([NotNull] Func<TEventArgs, Task> actionItem)
        {
            m_registeredActions.Remove(actionItem);
        }

        [NotNull]
        internal List<Func<TEventArgs, Task>> GetAllRegisteredEvents()
        {
            return m_registeredActions;
        }
    }
}
