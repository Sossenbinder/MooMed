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
	    private readonly ISearchService m_searchService;

	    public SearchController([NotNull] ISearchService searchService)
	    {
		    m_searchService = searchService;
	    }

        [ItemNotNull]
        public async Task<JsonResponse> SearchForQuery(string query)
        {
            var searchResult = await m_searchService.Search(query);

            var result = new
            {
                searchResult
            };

            return JsonResponse.Success(result);
        }
    }
}