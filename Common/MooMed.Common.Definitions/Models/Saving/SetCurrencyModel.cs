using MooMed.Common.Definitions.IPC;
using ProtoBuf;

namespace MooMed.Common.Definitions.Models.Saving
{
	[ProtoContract]
	public class SetCurrencyModel : SessionContextAttachedContainer
	{
		[ProtoMember(1)]
		public Currency Currency { get; set; }
	}
}