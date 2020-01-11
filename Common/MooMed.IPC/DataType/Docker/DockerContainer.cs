namespace MooMed.IPC.DataType.Docker
{
	public class DockerContainer : IStatefulEndpoint
	{
		public int InstanceNumber { get; set; }

		public string IpAddress { get; set; }
	}
}
