using System.Runtime.Serialization;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;
using ProtoBuf;

namespace MooMed.Common.Definitions.Models.Session
{
    [ProtoContract]
    public class SessionContext : ISessionContext
    {
        [ProtoMember(1)]
        public Account Account { get; set; }

        public int HashableIdentifier => Account.Id;

        public SessionContext()
        {
        }
    }
}
