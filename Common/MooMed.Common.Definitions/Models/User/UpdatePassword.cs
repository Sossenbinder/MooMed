using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.Session.Interface;
using ProtoBuf;

namespace MooMed.Common.Definitions.Models.User
{
	[ProtoContract]
	public class UpdatePassword : SessionContextAttachedContainer
	{
		[ProtoMember(1)]
		public string OldPassword { get; set; } = null!;

		[ProtoMember(2)]
		public string NewPassword { get; set; } = null!;
	}
}