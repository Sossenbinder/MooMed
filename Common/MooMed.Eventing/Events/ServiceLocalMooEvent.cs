using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MooMed.DotNet.Extensions;
using MooMed.DotNet.Utils.Tasks;
using MooMed.Eventing.Events.Interface;
using MooMed.Logging.Abstractions.Interface;

[assembly: InternalsVisibleTo("MooMed.Core.Tests")]

namespace MooMed.Eventing.Events
{
    public class ServiceLocalMooEvent<TEventArgs> : EventBase<TEventArgs>, ILocalEvent<TEventArgs>
    {
        public async Task<AccumulatedMooEventExceptions> Raise(TEventArgs eventArgs)
        {
            var accumulatedExceptions = new AccumulatedMooEventExceptions();

            await Handlers.ToArray().ParallelAsync(async handler =>
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

        public void RaiseFireAndForget(TEventArgs eventArgs, IMooMedLogger logger)
        {
            _ = FireAndForgetTask.Run(() => Raise(eventArgs), logger);
        }
    }
}