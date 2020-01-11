using System.Collections.Generic;
using JetBrains.Annotations;

namespace MooMed.IPC.DataType.Docker
{
	public class DockerStatefulSet : IStatefulCollection
	{
		public int ReplicaCount { get; }

		public IEnumerable<IStatefulEndpoint> StatefulEndpoints { get; set; }

		public DockerStatefulSet([NotNull] List<DockerContainer> statefulEndpoints)
		{
			ReplicaCount = statefulEndpoints.Count;
			StatefulEndpoints = statefulEndpoints;
		}
	}
}
