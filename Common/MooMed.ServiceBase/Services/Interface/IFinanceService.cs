using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.ServiceModel;
using System.Threading.Tasks;
using MooMed.Common.Definitions.Models.Finance;
using MooMed.Common.Definitions.Models.Portfolio;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Core.DataTypes;
using MooMed.ServiceBase.Definitions.Interface;

namespace MooMed.ServiceBase.Services.Interface
{
	[ServiceContract]
	public interface IFinanceService : IGrpcService
	{
		[OperationContract]
		Task<ServiceResponse<IEnumerable<ExchangeTraded>>> GetExchangeTradeds();

		[OperationContract]
		Task<ServiceResponse<Portfolio>> GetPortfolio(ISessionContext sessionContext);

		[OperationContract]
		Task<ServiceResponse> AddFondToPortfolio([NotNull] PortfolioItem portfolioItem);
	}
}