using System.Diagnostics.CodeAnalysis;
using MooMed.Common.Definitions.IPC.Interface;
using MooMed.Common.Definitions.Messages.Account;
using MooMed.Common.Definitions.Models.Session.Interface;
using ProtoBuf;

namespace MooMed.Common.Definitions.IPC
{
	[ProtoContract]
	[ProtoInclude(32, typeof (AddAsFriendMessage))]
	public abstract class SessionContextAttachedContainer : ISessionContextAttachedContainer
	{
		[NotNull]
		[ProtoMember(1)]
		public ISessionContext SessionContext { get; set; }
	}
}
