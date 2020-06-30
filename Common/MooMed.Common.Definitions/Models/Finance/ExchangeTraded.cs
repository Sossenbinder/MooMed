using MooMed.Common.Definitions.Interface;
using ProtoBuf;

namespace MooMed.Common.Definitions.Models.Finance
{
	[ProtoContract]
	public class ExchangeTraded : IModel
	{
		[ProtoMember(1)]
		public ExchangeTradedType Type { get; set; }

		[ProtoMember(2)]
		public string Isin { get; set; }

		[ProtoMember(3)]
		public string ProductFamily { get; set; }

		[ProtoMember(4)]
		public string XetraSymbol { get; set; }

		[ProtoMember(5)]
		public string ReutersCode { get; set; }

		[ProtoMember(6)]
		public string BloombergTicker { get; set; }

		[ProtoMember(7)]
		public double? FeePercentage { get; set; }

		[ProtoMember(8)]
		public double? OngoingCharges { get; set; }

		[ProtoMember(9)]
		public string ProfitUse { get; set; }

		[ProtoMember(10)]
		public string ReplicationMethod { get; set; }

		[ProtoMember(11)]
		public string FundCurrency { get; set; }

		[ProtoMember(12)]
		public string TradingCurrency { get; set; }

		[ProtoMember(13)]
		public string MQV { get; set; }

		[ProtoMember(14)]
		public double MaxSpread { get; set; }

		[ProtoMember(15)]
		public string ReutersInav { get; set; }

		[ProtoMember(16)]
		public string BloombergInav { get; set; }

		[ProtoMember(17)]
		public string Benchmark { get; set; }

		[ProtoMember(18)]
		public string Homepage { get; set; }
	}
}
