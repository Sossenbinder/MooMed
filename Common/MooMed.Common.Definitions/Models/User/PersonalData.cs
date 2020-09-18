using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.Session.Interface;
using ProtoBuf;

namespace MooMed.Common.Definitions.Models.User
{
	[ProtoContract]
	public class PersonalData : SessionContextAttachedContainer
	{
		[ProtoMember(1)]
		public string? Email { get; set; }

		[ProtoMember(2)]
		public string? UserName { get; set; }

		[ProtoMember(3)]
		public string? Password { get; set; }
	}
}