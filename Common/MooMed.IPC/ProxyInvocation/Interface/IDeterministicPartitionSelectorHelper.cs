using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;

namespace MooMed.IPC.ProxyInvocation.Interface
{
	/// <summary>
	/// Boils down a given hash identifier to an actual instance number
	/// </summary>
	public interface IDeterministicPartitionSelectorHelper
	{
		int HashIdentifierToPartitionIntIdentifier(int hashableIdentifier, int replicas);

		int HashIdentifierToPartitionIntIdentifier([NotNull] IEndpointSelector endpointSelector, int replicas);
	}
}
