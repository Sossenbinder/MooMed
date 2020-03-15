using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MooMed.Module.Finance.Database.Entities
{
	[Table("Stocks")]
	public class StockEntity
	{
		public string Symbol { get; }

		public string Name { get; }
	}
}
