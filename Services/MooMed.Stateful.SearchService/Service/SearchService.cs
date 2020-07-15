using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.Search;
using MooMed.Common.ServiceBase;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Core.DataTypes;
using MooMed.Logging.Loggers.Interface;

namespace MooMed.Stateful.SearchService.Service
{
    public class SearchService : MooMedServiceBase, ISearchService
    {
	    [NotNull]
	    private readonly IAccountService _accountService;

	    public SearchService(
            [NotNull] IMooMedLogger logger,
            [NotNull] IAccountService accountService)
            : base(logger)
        {
	        _accountService = accountService;
        }

        [ItemNotNull]
        public async Task<ServiceResponse<SearchResult>> Search([NotNull] string query)
        {
	        var accountsStartingWithNameResponse = await _accountService.FindAccountsStartingWithName(query);

            var searchResult = new SearchResult
            {
                CorrespondingUsers = accountsStartingWithNameResponse.PayloadOrNull,
            };

            return ServiceResponse<SearchResult>.Success(searchResult);
        }
    }
}
