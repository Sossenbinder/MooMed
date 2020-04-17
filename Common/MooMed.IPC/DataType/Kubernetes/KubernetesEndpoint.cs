namespace MooMed.IPC.DataType.Kubernetes
{
	public class KubernetesEndpoint : IStatefulEndpoint
	{
		public int InstanceNumber { get; set; }

		public string IpAddress { get; set; }
	}
}