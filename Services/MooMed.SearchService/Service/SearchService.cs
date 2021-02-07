using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.Search;
using MooMed.Common.ServiceBase;
using MooMed.Core.DataTypes;
using MooMed.Logging.Abstractions.Interface;
using MooMed.ServiceBase.Services.Interface;
using System.Threading.Tasks;

namespace MooMed.SearchService.Service
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

		public async Task<ServiceResponse<SearchResult>> Search(string query)
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