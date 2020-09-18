using System.Diagnostics.CodeAnalysis;
using MooMed.Common.Definitions.Interface;
using MooMed.Common.Definitions.Models.Session.Interface;
using ProtoBuf;

namespace MooMed.Common.Definitions.IPC.Interface
{
	[ProtoContract]
	[ProtoInclude(100, typeof(SessionContextAttachedContainer))]
	public interface ISessionContextAttachedContainer : IModel
	{
		[NotNull]
		public ISessionContext SessionContext { get; set; }
	}
}