using System.Collections.Generic;
using MooMed.Common.Definitions.Models.User;

namespace MooMed.Web.DataTypes
{
    public class SearchResult
    {
        public List<Account> CorrespondingUsers { get; set; }
    }
}