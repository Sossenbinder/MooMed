using ProtoBuf;

namespace MooMed.Common.Definitions.Models.User
{
	[ProtoContract]
	public class AccountValidationMailData
	{
		[ProtoMember(1)]
		public Account Account { get; set; }

		[ProtoMember(2)]
		public Language Language { get; set; }
	}
}