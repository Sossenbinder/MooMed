using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.Search;
using MooMed.Core.DataTypes;
using MooMed.IPC.Grpc.Interface;
using MooMed.RemotingProxies.ProxyInvocation;
using MooMed.ServiceBase.Services.Interface;

namespace MooMed.RemotingProxies.Proxies
{
	public class SearchServiceProxy : AbstractDeploymentProxy<ISearchService>, ISearchService
	{
		public SearchServiceProxy([NotNull] IGrpcClientProvider grpcClientProvider)
			: base(grpcClientProvider,
				DeploymentService.SearchService)
		{
		}

		public Task<ServiceResponse<SearchResult>> Search(string query)
			=> InvokeWithResult(service => service.Search(query));
	}
}