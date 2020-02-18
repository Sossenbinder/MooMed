using System.Collections.Generic;
using MooMed.Common.Definitions.Models.User;
using ProtoBuf;

namespace MooMed.Common.Definitions.Models.Search
{
    [ProtoContract]
    public class SearchResult
    {
        [ProtoMember(1)]
        public List<Account> CorrespondingUsers { get; set; }
    }
}