using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.Search;
using MooMed.Core.DataTypes;
using MooMed.Grpc.Services.Interface;
using MooMed.IPC.Grpc.Interface;
using MooMed.IPC.ProxyInvocation;

namespace MooMed.Stateful.SearchService.Remoting
{
	public class SearchServiceProxy : AbstractDeploymentProxy<ISearchService>, ISearchService
	{
		public SearchServiceProxy([NotNull] IGrpcClientProvider grpcClientProvider)
			: base(grpcClientProvider,
				MooMedService.SearchService)
		{
		}

		public Task<ServiceResponse<SearchResult>> Search(string query)
			=> InvokeWithResult(service => service.Search(query));
	}
}
