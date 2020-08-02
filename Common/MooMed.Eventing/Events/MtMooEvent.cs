using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.DotNet.Extensions;
using MooMed.Eventing.Events.MassTransit.Interface;
using MooMed.Logging.Loggers;

namespace MooMed.Eventing.Events
{
	public class MtMooEvent<TEventArgs> : EventBase<TEventArgs> 
		where TEventArgs : class
	{
		[NotNull]
		private readonly IMassTransitEventingService _massTransitEventingService;

		public MtMooEvent(
			[NotNull] string queueName,
			[NotNull] IMassTransitEventingService massTransitEventingService)
		{
			_massTransitEventingService = massTransitEventingService;

			_massTransitEventingService.RegisterForEvent<TEventArgs>(queueName, OnEventReceived);
		}

		private async Task OnEventReceived(TEventArgs eventArgs)
		{
			try
			{
				await Handlers.ParallelAsync(handler => handler(eventArgs));
			}
			catch (Exception e)
			{
				StaticLogger.Error(e.Message);
			}
		}

		public override async Task<AccumulatedMooEventExceptions> Raise(TEventArgs eventArgs)
		{
			await _massTransitEventingService.RaiseEvent(eventArgs);

			return new AccumulatedMooEventExceptions();
		}
	}
}
