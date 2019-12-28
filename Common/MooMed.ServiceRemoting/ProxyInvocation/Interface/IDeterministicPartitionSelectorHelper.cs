using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;

namespace MooMed.ServiceRemoting.ProxyInvocation.Interface
{
	public interface IDeterministicPartitionSelectorHelper
	{
		long HashIdentifierToPartitionIntIdentifier(int hashableIdentifier, long completeRange);

		long HashIdentifierToPartitionIntIdentifier([NotNull] IPartitionSelector partitionSelector, long completeRange);
	}
}
