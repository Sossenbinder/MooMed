using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Web.Controllers.Base;
using MooMed.Web.Controllers.Result;

namespace MooMed.Web.Controllers
{
    public class SearchController : BaseController
    {
	    [NotNull]
	    private readonly ISearchService _searchService;

	    public SearchController([NotNull] ISearchService searchService)
	    {
		    _searchService = searchService;
	    }

        [ItemNotNull]
        public async Task<JsonResponse> SearchForQuery(string query)
        {
            var searchResult = await _searchService.Search(query);
            
            return searchResult.ToJsonResponse();
        }
    }
}