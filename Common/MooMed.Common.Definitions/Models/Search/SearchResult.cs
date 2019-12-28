using System.Collections.Generic;
using MooMed.Common.Definitions.Models.User;

namespace MooMed.Common.Definitions.Models.Search
{
    public class SearchResult
    {
        public List<Account> CorrespondingUsers { get; set; }
    }
}