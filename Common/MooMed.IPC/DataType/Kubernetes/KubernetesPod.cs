namespace MooMed.IPC.DataType.Kubernetes
{
	public class KubernetesPod : IStatefulEndpoint
	{
		public int InstanceNumber { get; set; }

		public string IpAddress { get; set; }
	}

	public class KubernetesStatefulPod : KubernetesPod
	{
		public int StatefulSetPodInstance { get; set; }
	}
}
