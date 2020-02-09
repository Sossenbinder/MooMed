using System.ServiceModel;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.Search;
using MooMed.Grpc.Definitions.Interface;

namespace MooMed.Common.ServiceBase.Interface
{
	[ServiceContract]
    public interface ISearchService : IGrpcService
    {
        [OperationContract]
        [NotNull]
        Task<SearchResult> Search(string query);
    }
}