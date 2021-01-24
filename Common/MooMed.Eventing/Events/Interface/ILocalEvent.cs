using System.Threading.Tasks;
using MooMed.Logging.Abstractions.Interface;

namespace MooMed.Eventing.Events.Interface
{
	public interface ILocalEvent : IEvent
	{
		Task Raise();

		void RaiseFireAndForget(IMooMedLogger logger);
	}

	public interface ILocalEvent<TEventArgs> : IEvent<TEventArgs>
	{
		Task Raise(TEventArgs eventArgs);

		void RaiseFireAndForget(TEventArgs eventArgs, IMooMedLogger logger);
	}
}