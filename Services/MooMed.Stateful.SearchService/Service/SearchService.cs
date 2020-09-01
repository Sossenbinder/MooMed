using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Logging;
using MooMed.Common.Definitions.Models.Search;
using MooMed.Common.ServiceBase.ServiceBase;
using MooMed.Core.DataTypes;
using MooMed.ServiceBase.Services.Interface;

namespace MooMed.Stateful.SearchService.Service
{
	public class SearchService : MooMedServiceBaseWithLogger, ISearchService
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