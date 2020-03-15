namespace MooMed.Common.Definitions.IPC
{
	public class ClusterChangeEvent
	{
		public StatefulSet StatefulSet { get; set; }

		public int NewReplicaAmount { get; set; }
	}
}
