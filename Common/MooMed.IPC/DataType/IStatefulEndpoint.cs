using System;
using System.Collections.Generic;
using System.Text;

namespace MooMed.IPC.DataType
{
	public interface IStatefulEndpoint
	{
		public int InstanceNumber { get; set; }

		public string IpAddress { get; set; }
	}
}
