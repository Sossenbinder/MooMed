using System.Collections.Generic;
using JetBrains.Annotations;

namespace MooMed.IPC.DataType.Kubernetes
{
	public class KubernetesStatefulSet : IStatefulCollection
	{
		public int ReplicaCount { get; }

		public IEnumerable<IStatefulEndpoint> StatefulEndpoints { get; set; }

		public KubernetesStatefulSet([NotNull] IReadOnlyCollection<KubernetesEndpoint> statefulEndpoints)
		{
			ReplicaCount = statefulEndpoints.Count;
			StatefulEndpoints = statefulEndpoints;
		}
	}
}
