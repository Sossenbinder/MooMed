using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.DotNet.Extensions;

[assembly: InternalsVisibleTo("MooMed.Core.Tests")]

namespace MooMed.Eventing.Events
{
	public class ServiceLocalMooEvent<TEventArgs> : EventBase<TEventArgs>
	{
		[ItemNotNull]
		public override async Task<AccumulatedMooEventExceptions> Raise(TEventArgs eventArgs)
		{
			var accumulatedExceptions = new AccumulatedMooEventExceptions();

			await Handlers.ParallelAsync(async handler =>
			{
				try
				{
					await handler(eventArgs);
				}
				catch (Exception e)
				{
					accumulatedExceptions.Exceptions.Add(e);
				}
			});

			return accumulatedExceptions;
		}
	}
}