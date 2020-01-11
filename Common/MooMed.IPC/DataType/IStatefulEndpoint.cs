namespace MooMed.IPC.DataType
{
	public interface IStatefulEndpoint
	{
		public int InstanceNumber { get; set; }

		public string IpAddress { get; set; }
	}
}
