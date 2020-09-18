using System.Diagnostics.CodeAnalysis;
using MooMed.Common.Definitions.IPC.Interface;
using MooMed.Common.Definitions.Models.Session.Interface;
using ProtoBuf;

namespace MooMed.Common.Definitions.IPC
{
	[ProtoContract]
	public abstract class SessionContextAttachedContainer : ISessionContextAttachedContainer
	{
		[NotNull]
		[ProtoMember(50)]
		public ISessionContext SessionContext { get; set; } = null!;
	}
}