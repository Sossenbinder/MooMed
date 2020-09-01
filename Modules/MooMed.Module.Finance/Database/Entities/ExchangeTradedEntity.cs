using System.ComponentModel.DataAnnotations.Schema;
using MooMed.Common.Definitions.Interface;
using MooMed.Common.Definitions.Models.Finance;

namespace MooMed.Module.Finance.Database.Entities
{
	[Table("ExchangeTradeds")]
	public class ExchangeTradedEntity : IEntity<string>
	{
		[Column("Id")]
		public string? Id { get; set; }

		[Column("Type")]
		public ExchangeTradedType Type { get; set; }

		[Column("Isin")]
		public string? Isin { get; set; }

		[Column("ProductFamily")]
		public string? ProductFamily { get; set; }

		[Column("XetraSymbol")]
		public string? XetraSymbol { get; set; }

		[Column("ReutersCode")]
		public string? ReutersCode { get; set; }

		[Column("BloombergTicker")]
		public string? BloombergTicker { get; set; }

		[Column("FeePercentage")]
		public double? FeePercentage { get; set; }

		[Column("OngoingCharges")]
		public double? OngoingCharges { get; set; }

		[Column("ProfitUse")]
		public string? ProfitUse { get; set; }

		[Column("ReplicationMethod")]
		public string? ReplicationMethod { get; set; }

		[Column("FundCurrency")]
		public string? FundCurrency { get; set; }

		[Column("TradingCurrency")]
		public string? TradingCurrency { get; set; }

		[Column("MQV")]
		public string? MQV { get; set; }

		[Column("MaxSpread")]
		public double MaxSpread { get; set; }

		[Column("ReutersInav")]
		public string? ReutersInav { get; set; }

		[Column("BloombergInav")]
		public string? BloombergInav { get; set; }

		[Column("Benchmark")]
		public string? Benchmark { get; set; }

		[Column("Homepage")]
		public string? Homepage { get; set; }
	}
}