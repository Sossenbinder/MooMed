using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.IPC.DataType;

namespace MooMed.IPC.EndpointResolution.Interface
{
	public interface IStatefulCollectionInfoProvider
	{
		[ItemNotNull]
		Task<IStatefulCollection> GetStatefulCollectionInfoForService(DeployedService deployedService);

		Task<int> GetAvailableReplicasForService(DeployedService deployedService);
	}
}
