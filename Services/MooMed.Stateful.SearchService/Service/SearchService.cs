using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.Search;
using MooMed.Common.ServiceBase;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Core.Code.Logging.Loggers.Interface;

namespace MooMed.Stateful.SearchService.Service
{
    public class SearchService : MooMedServiceBase, ISearchService
    {
	    [System.Diagnostics.CodeAnalysis.NotNull]
	    private readonly IAccountService m_accountService;

	    public SearchService(
            [System.Diagnostics.CodeAnalysis.NotNull] IMainLogger logger,
            [System.Diagnostics.CodeAnalysis.NotNull] IAccountService accountService)
            : base(logger)
        {
	        m_accountService = accountService;
        }

        [ItemNotNull]
        public async Task<SearchResult> Search([System.Diagnostics.CodeAnalysis.NotNull] string query)
        {
            var searchResult = new SearchResult
            {
                CorrespondingUsers = await m_accountService.FindAccountsStartingWithName(query)
            };

            return searchResult;
        }
    }
}
