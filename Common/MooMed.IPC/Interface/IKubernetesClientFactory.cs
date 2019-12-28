using k8s;

namespace MooMed.IPC.Interface
{
	/// <summary>
	/// Responsible for creating the Kubernetes client
	/// </summary>
	public interface IKubernetesClientFactory
	{
		Kubernetes GetClient();
	}
}
