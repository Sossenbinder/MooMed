using MooMed.Common.Definitions.Interface;
using ProtoBuf;

namespace MooMed.Common.Definitions.Models.Finance
{
	[ProtoContract]
	public class GetExchangeTradedsModel : IModel
	{
		[ProtoMember(1)]
		public ExchangeTradedType? ExchangeTradedType { get; set; }
	}
}
