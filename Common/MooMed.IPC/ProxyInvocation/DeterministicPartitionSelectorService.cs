using MooMed.Common.Definitions.IPC;
using MooMed.IPC.ProxyInvocation.Interface;

namespace MooMed.IPC.ProxyInvocation
{
	public class DeterministicPartitionSelectorService : IDeterministicPartitionSelectorHelper
	{
		public int HashIdentifierToPartitionIntIdentifier(IEndpointSelector endpointSelector, int replicas)
			=> HashIdentifierToPartitionIntIdentifier(endpointSelector.HashableIdentifier, replicas);

		public int HashIdentifierToPartitionIntIdentifier(int hashableIdentifier, int replicas)
		{
			return hashableIdentifier % replicas;
		}
	}
}
