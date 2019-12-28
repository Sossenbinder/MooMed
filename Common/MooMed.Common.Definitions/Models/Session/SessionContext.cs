using System.Runtime.Serialization;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;

namespace MooMed.Common.Definitions.Models.Session
{
    [DataContract]
    public class SessionContext : ISessionContext
    {
        [DataMember]
        public Account Account { get; set; }

        public int HashableIdentifier => Account.Id;
    }
}
