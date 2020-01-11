using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.IPC.DataType;

namespace MooMed.IPC.EndpointResolution.Interface
{
	public interface IStatefulCollectionDiscovery
	{
		IAsyncEnumerable<(DeployedService, IStatefulCollection)> RefreshForAllStatefulSets();

		[ItemNotNull]
		Task<IStatefulCollection> GetStatefulSetInfo(DeployedService deployedService);
	}
}
