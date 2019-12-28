using System.Collections.Generic;

namespace MooMed.IPC.DataType
{
	public interface IStatefulCollection
	{
		public int ReplicaCount { get; }

		public IEnumerable<IStatefulEndpoint> StatefulEndpoints { get; set; }
	}
}
