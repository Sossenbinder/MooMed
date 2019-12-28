using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.Search;
using MooMed.Grpc.Definitions.Interface;

namespace MooMed.Common.ServiceBase.Interface
{
    public interface ISearchService : IGrpcService
    {
        [NotNull]
        Task<SearchResult> Search(string query);
    }
}