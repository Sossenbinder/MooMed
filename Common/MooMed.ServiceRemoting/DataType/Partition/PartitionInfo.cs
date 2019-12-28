namespace MooMed.ServiceRemoting.DataType.Partition
{
	public class PartitionInfo
	{
		public long Low { get; }

		public long High { get; }

		public long RangeSize => High - Low;

		public PartitionInfo(long low, long high)
		{
			Low = low;
			High = high;
		}
	}
}
