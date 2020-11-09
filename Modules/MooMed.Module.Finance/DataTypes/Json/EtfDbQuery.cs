using System;
using System.Collections.Generic;
using System.Text;

namespace MooMed.Module.Finance.DataTypes.Json
{
	public class EtfDbQuery
	{
		public int page { get; set; }

		public int per_page { get; set; }

		public string[] structure { get; set; } = null!;

		public string[] only { get; set; } = null!;
	}
}