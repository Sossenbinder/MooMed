using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MooMed.DotNet.Extensions;
using MooMed.DotNet.Utils.Tasks;
using MooMed.Eventing.Events.Interface;
using MooMed.Logging.Abstractions.Interface;

[assembly: InternalsVisibleTo("MooMed.Core.Tests")]

namespace MooMed.Eventing.Events
{
	public class ServiceLocalEvent : EventBase, ILocalEvent
	{
		public async Task Raise()
		{
			var exceptionList = new List<Exception>();

			await Handlers.ToArray().ParallelAsync(async handler =>
			{
				try
				{
					await handler();
				}
				catch (Exception e)
				{
					exceptionList.Add(e);
				}
			});

			if (exceptionList.Any())
			{
				throw new AggregateException(exceptionList);
			}
		}

		public void RaiseFireAndForget(IMooMedLogger logger)
		{
			_ = FireAndForgetTask.Run(Raise, logger);
		}
	}

	public class ServiceLocalEvent<TEventArgs> : EventBase<TEventArgs>, ILocalEvent<TEventArgs>
	{
		public async Task Raise(TEventArgs eventArgs)
		{
			var exceptionList = new List<Exception>();

			await Handlers.ToArray().ParallelAsync(async handler =>
			{
				try
				{
					await handler(eventArgs);
				}
				catch (Exception e)
				{
					exceptionList.Add(e);
				}
			});

			if (exceptionList.Any())
			{
				throw new AggregateException(exceptionList);
			}
		}

		public void RaiseFireAndForget(TEventArgs eventArgs, IMooMedLogger logger)
		{
			_ = FireAndForgetTask.Run(() => Raise(eventArgs), logger);
		}
	}
}