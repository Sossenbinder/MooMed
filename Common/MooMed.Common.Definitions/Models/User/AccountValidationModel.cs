using MooMed.Common.Definitions.Interface;
using ProtoBuf;

namespace MooMed.Common.Definitions.Models.User
{
	[ProtoContract]
	public class AccountValidationModel : IModel
	{
		[ProtoMember(1)]
		public int AccountId { get; set; }

		[ProtoMember(2)]
		public string Token { get; set; }
	}
}
