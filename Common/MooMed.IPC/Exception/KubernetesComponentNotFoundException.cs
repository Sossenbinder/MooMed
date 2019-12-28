using JetBrains.Annotations;

namespace MooMed.IPC.Exception
{
	public class KubernetesComponentNotFoundException : System.Exception
	{
		public KubernetesComponentNotFoundException([NotNull] string message)
			:base(message)
		{

		}
	}
}
