using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace MooMed.IPC.DataType.Kubernetes
{
	public class KubernetesStatefulSet : IStatefulCollection
	{
		public int ReplicaCount { get; }

		public IEnumerable<IStatefulEndpoint> StatefulEndpoints { get; set; }

		public KubernetesStatefulSet([NotNull] List<KubernetesStatefulPod> statefulEndpoints)
		{
			ReplicaCount = statefulEndpoints.Count;
			StatefulEndpoints = statefulEndpoints;
		}
	}
}
