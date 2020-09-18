using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.Session.Interface;
using ProtoBuf;

namespace MooMed.Common.Definitions.Models.User
{
	[ProtoContract]
	public class AddAsFriendModel : SessionContextAttachedContainer
	{
		[ProtoMember(1)]
		public int AccountId { get; set; }
	}
}