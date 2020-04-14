using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Core.Code.Logging.Loggers;
using MooMed.Eventing.Events.Interface;
using MooMed.Eventing.Events.MassTransit.Interface;

namespace MooMed.Eventing.Events
{
	public class MtMooEvent<TEventArgs> : IAwaitableEvent<TEventArgs> 
		where TEventArgs : class
	{
		[NotNull]
		private readonly IMassTransitEventingService m_massTransitEventingService;

		[NotNull]
		private readonly List<Func<TEventArgs, Task>> m_handlers;

		public MtMooEvent(
			[NotNull] string queueName,
			[NotNull] IMassTransitEventingService massTransitEventingService)
		{
			m_massTransitEventingService = massTransitEventingService;
			m_handlers = new List<Func<TEventArgs, Task>>();

			m_massTransitEventingService.RegisterForEvent<TEventArgs>(queueName, OnEventReceived);
		}

		private async Task OnEventReceived(TEventArgs eventArgs)
		{
			try
			{
				var tasks = m_handlers.Select(handler => handler(eventArgs));

				await Task.WhenAll(tasks);
			}
			catch (Exception e)
			{
				StaticLogger.Error(e.Message);
			}
		}

		public async Task<AccumulatedMooEventExceptions> Raise(TEventArgs eventArgs)
		{
			await m_massTransitEventingService.RaiseEvent(eventArgs);

			return new AccumulatedMooEventExceptions();
		}

		public void Register(Action<TEventArgs> handler)
		{
			Register(eventArgs =>
			{
				handler(eventArgs);
				return Task.CompletedTask;
			});
		}

		public void Register(Func<TEventArgs, Task> handler)
		{
			m_handlers.Add(handler);
		}

		public void UnRegister(Func<TEventArgs, Task> handler)
		{
			m_handlers.Remove(handler);
		}
	}
}
