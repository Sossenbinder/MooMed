using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.Search;
using MooMed.ServiceBase.Service.Interface.Base;

namespace MooMed.ServiceBase.Service.Interface
{
	public interface ISearchServiceProxy : ISearchService
	{

	}

    public interface ISearchService : IRemotingServiceBase
	{
        [NotNull]
        Task<SearchResult> Search(string query);
    }
}