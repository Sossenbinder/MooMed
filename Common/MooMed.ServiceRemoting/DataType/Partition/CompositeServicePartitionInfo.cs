using System;
using System.Collections.Generic;
using System.Linq;

namespace MooMed.ServiceRemoting.DataType.Partition
{
	public class CompositeServicePartitionInfo
	{
		public DeployedFabricService CorrespondingService { get; }

		public List<PartitionInfo> Partitions { get; set; }

		public long High => Partitions.Max(x => x.High);

		public long Low => Partitions.Min(x => x.Low);

		public long Range => High - Low;

		public CompositeServicePartitionInfo(DeployedFabricService correspondingService)
		{
			Partitions = new List<PartitionInfo>();

			CorrespondingService = correspondingService;
		}
	}
}
