using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.Session.Interface;
using ProtoBuf;

namespace MooMed.Common.Definitions.Models.Portfolio
{
	[ProtoContract]
	public class PortfolioItem : SessionContextAttachedContainer
	{
		[ProtoMember(1)]
		public string Isin { get; set; } = null!;

		[ProtoMember(2)]
		public float Amount { get; set; }
	}
}