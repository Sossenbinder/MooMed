using System.Diagnostics.CodeAnalysis;
using MooMed.Common.Definitions.IPC.Interface;
using MooMed.Common.Definitions.Models.Session.Interface;
using ProtoBuf;

namespace MooMed.Common.Definitions.IPC
{
	[ProtoContract]
	public abstract class SessionContextAttachedDataType : ISessionContextAttachedDataType
	{
		[NotNull]
		[ProtoMember(1)]
		public ISessionContext SessionContext { get; set; }

		protected SessionContextAttachedDataType([NotNull] ISessionContext sessionContext) =>
			SessionContext = sessionContext;
	}
}
