using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.Search;
using MooMed.Common.ServiceBase.Interface;
using MooMed.IPC.EndpointResolution.Interface;
using MooMed.IPC.ProxyInvocation;
using MooMed.IPC.ProxyInvocation.Interface;

namespace MooMed.Stateful.SearchService.Remoting
{
	public class SearchServiceProxy : AbstractProxy<ISearchService>, ISearchService
	{
		public SearchServiceProxy(
			[NotNull] IStatefulCollectionInfoProvider statefulCollectionInfoProvider,
			[NotNull] IGrpcClientProvider grpcClientProvider,
			[NotNull] IDeterministicPartitionSelectorHelper deterministicPartitionSelectorHelper)
			: base(statefulCollectionInfoProvider,
				grpcClientProvider,
				deterministicPartitionSelectorHelper,
				DeployedService.SearchService)
		{
		}

			public Task<SearchResult> Search(string query)
			=> InvokeOnRandomReplica(service => service.Search(query));
	}
}
