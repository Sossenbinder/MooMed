//----------------------
//This file was autogenerated by GrpcProxyGenerator.Service.GrpcProxyFactory
//Timestamp of generation: UTC 23-Nov-20 19:16:58
//----------------------

namespace MooMed.RemotingProxies.Proxies
{
	public class SearchServiceProxy : MooMed.RemotingProxies.ProxyInvocation.AbstractDeploymentProxy<MooMed.ServiceBase.Services.Interface.ISearchService>, MooMed.ServiceBase.Services.Interface.ISearchService
	{
		public SearchServiceProxy(MooMed.IPC.Grpc.Interface.IGrpcClientProvider clientProvider)
			: base(clientProvider,
				MooMed.Common.Definitions.IPC.DeploymentService.SearchService)
		{ }

		public System.Threading.Tasks.Task<MooMed.Core.DataTypes.ServiceResponse<MooMed.Common.Definitions.Models.Search.SearchResult>> Search(
			System.String query)
			=> InvokeWithResult(service => service.Search(
				query));

	}
}
