using System.Threading.Tasks;
using MooMed.Common.Definitions.Logging;

namespace MooMed.Eventing.Events.Interface
{
    public interface ILocalEvent<TEventArgs> : IEvent<TEventArgs>
    {
        Task<AccumulatedMooEventExceptions> Raise(TEventArgs eventArgs);

        void RaiseFireAndForget(TEventArgs eventArgs, IMooMedLogger logger);
    }
}