using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.User;
using ProtoBuf;

namespace MooMed.Common.Definitions.Models.Session.Interface
{
	[ProtoContract]
	[ProtoInclude(100, typeof(SessionContext))]
	public interface ISessionContext : IEndpointSelector
	{
		[ProtoMember(1)]
		Account Account { get; set; }
	}
}