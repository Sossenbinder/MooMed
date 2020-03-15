using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.IPC.DataType;

namespace MooMed.IPC.EndpointResolution.Interface
{
	public interface IStatefulCollectionDiscovery
	{
		[ItemNotNull]
		Task<IStatefulCollection> GetStatefulSetInfo(StatefulSet statefulSet, int totalReplicas = 1);
	}
}
