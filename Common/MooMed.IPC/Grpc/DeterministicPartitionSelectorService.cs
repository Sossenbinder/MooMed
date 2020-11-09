using MooMed.Common.Definitions.IPC;
using MooMed.IPC.Grpc.Interface;

namespace MooMed.IPC.Grpc
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