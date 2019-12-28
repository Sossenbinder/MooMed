using System.Fabric;
using MooMed.Common.Definitions.IPC;
using MooMed.ServiceRemoting.ProxyInvocation.Interface;

namespace MooMed.ServiceRemoting.ProxyInvocation
{
	public class DeterministicPartitionSelectorService : IDeterministicPartitionSelectorHelper
	{
		public long HashIdentifierToPartitionIntIdentifier(IPartitionSelector partitionSelector, long completeRange)
			=> HashIdentifierToPartitionIntIdentifier(partitionSelector.HashableIdentifier, completeRange);

		public long HashIdentifierToPartitionIntIdentifier(int hashableIdentifier, long completeRange)
		{
			return completeRange % hashableIdentifier;
		}
	}
}
