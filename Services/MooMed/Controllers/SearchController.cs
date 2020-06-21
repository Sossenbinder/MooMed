﻿using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MooMed.Common.Definitions.Models.Search;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.Definitions.UiModels.Search;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Core.Code.Extensions;
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
        [Authorize]
        public async Task<JsonResponse> SearchForQuery([NotNull] [FromBody] SearchUiModel searchUiModel)
        {
	        var query = searchUiModel.Query;
            
            if (query.IsNullOrEmpty())
	        {
                return JsonResponse.Success(new SearchResult()
                {
                    CorrespondingUsers = new List<Account>(),
                });
	        }

            var searchResult = await _searchService.Search(query);
            
            return searchResult.ToJsonResponse();
        }
    }
}