namespace MooMed.Common.Definitions.IPC
{
	public class ClusterChangeEvent
	{
		public StatefulSetService StatefulSetService { get; set; }

		public int NewReplicaAmount { get; set; }
	}
}
