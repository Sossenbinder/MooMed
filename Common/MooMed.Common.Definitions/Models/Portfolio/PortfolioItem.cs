using MooMed.Common.Definitions.Interface;
using MooMed.Common.Definitions.IPC;
using ProtoBuf;

namespace MooMed.Common.Definitions.Models.Portfolio
{
	[ProtoContract]
	public class PortfolioItem : SessionContextAttachedContainer, IModel
	{
		[ProtoMember(2)]
		public string Isin { get; set; }

		[ProtoMember(3)]
		public float Amount { get; set; }
	}
}