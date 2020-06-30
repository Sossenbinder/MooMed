using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.ServiceModel;
using System.Threading.Tasks;
using MooMed.Common.Definitions.Models.Finance;
using MooMed.Common.Definitions.Models.Portfolio;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Core.DataTypes;
using MooMed.Grpc.Definitions.Interface;

namespace MooMed.Common.ServiceBase.Interface
{
	[ServiceContract]
	public interface IFinanceService : IGrpcService
	{
		[OperationContract]
		Task<ServiceResponse<IEnumerable<ExchangeTraded>>> GetExchangeTradeds();

		[OperationContract]
		Task<ServiceResponse<Portfolio>> GetPortfolio([NotNull] ISessionContext sessionContext);

		[OperationContract]
		Task<ServiceResponse> AddFondToPortfolio([NotNull] PortfolioItem portfolioItem);

	}
}
