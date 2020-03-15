using System;
using System.Collections.Generic;
using System.Text;

namespace MooMed.Module.Finance.DataTypes.Json
{
	public class EtfDbQuery
	{
		public int page { get; set; }

		public int per_page { get; set; }

		public string[] structure { get; set; }

		public string[] only { get; set; }
	}
}
