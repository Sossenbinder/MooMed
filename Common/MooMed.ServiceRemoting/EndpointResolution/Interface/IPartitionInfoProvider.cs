using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.ServiceRemoting.DataType;
using MooMed.ServiceRemoting.DataType.Partition;

namespace MooMed.ServiceRemoting.EndpointResolution.Interface
{
	public interface IPartitionInfoProvider
	{
		[ItemNotNull]
		Task<CompositeServicePartitionInfo> GetPartitionInfoForService(DeployedFabricService deployedFabricService);
	}
}
