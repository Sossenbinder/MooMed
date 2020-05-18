using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;

namespace MooMed.IPC.ProxyInvocation.Interface
{
	/// <summary>
	/// Boils down a given hash identifier to an actual instance number
	/// </summary>
	public interface IDeterministicPartitionSelectorHelper
	{
		/// <summary>
		/// Turn the hashable identifier into a replica number
		/// </summary>
		/// <param name="hashableIdentifier">Hashable identifier to hash</param>
		/// <param name="replicas">Amount of replicas available</param>
		/// <returns>Identifier turned to an actual replica</returns>
		int HashIdentifierToPartitionIntIdentifier(int hashableIdentifier, int replicas);

		/// <summary>
		/// Turn the hashable identifier into a replica number
		/// </summary>
		/// <param name="endpointSelector">Endpoint selector to get the hashable identifier from</param>
		/// <param name="replicas">Amount of replicas available</param>
		/// <returns>Identifier turned to an actual replica</returns>
		int HashIdentifierToPartitionIntIdentifier([NotNull] IEndpointSelector endpointSelector, int replicas);
	}
}
