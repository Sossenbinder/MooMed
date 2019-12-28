using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.Search;
using MooMed.Common.ServiceBase.Interface;

namespace MooMed.Module.Search.Service
{
    public class SearchService : ISearchService
    {
        [NotNull]
        private readonly IAccountService m_accountService;

        public SearchService([NotNull] IAccountService accountService)
        {
            m_accountService = accountService;
        }

        [ItemNotNull]
        public async Task<SearchResult> Search([NotNull] string query)
        {
            var searchResult = new SearchResult
            {
                CorrespondingUsers = await m_accountService.FindAccountsStartingWithName(query)
            };

            return searchResult;
        }
    }
}