using System.ServiceModel;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.Search;
using MooMed.Core.DataTypes;
using MooMed.ServiceBase.Definitions.Interface;

namespace MooMed.ServiceBase.Services.Interface
{
	[ServiceContract]
	public interface ISearchService : IGrpcService
	{
		[OperationContract]
		[NotNull]
		Task<ServiceResponse<SearchResult>> Search(string query);
	}
}