using System.Diagnostics.CodeAnalysis;
using MooMed.Common.Definitions.Models.Session.Interface;
using ProtoBuf;

namespace MooMed.Common.Definitions.IPC.Interface
{
	[ProtoContract]
	[ProtoInclude(100, typeof(SessionContextAttachedDataType))]
	public interface ISessionContextAttachedDataType
	{
		[NotNull]
		public ISessionContext SessionContext { get; set; }
	}
}
