using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.Finance;
using MooMed.Common.Definitions.Models.Portfolio;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Core.DataTypes;
using MooMed.IPC.Grpc.Interface;
using MooMed.IPC.ProxyInvocation;

namespace MooMed.FinanceService.Remoting
{
	public class FinanceServiceProxy : AbstractDeploymentProxy<IFinanceService>, IFinanceService
	{
		public FinanceServiceProxy([NotNull] IGrpcClientProvider clientProvider) 
			: base(clientProvider, MooMedService.FinanceService)
		{
		}

		public Task<ServiceResponse<IEnumerable<ExchangeTraded>>> GetExchangeTradeds()
			=> InvokeWithResult(x => x.GetExchangeTradeds());

		public Task<ServiceResponse<Portfolio>> GetPortfolio(ISessionContext sessionContext)
			=> InvokeWithResult(x => x.GetPortfolio(sessionContext));

		public Task<ServiceResponse> AddFondToPortfolio(PortfolioItem portfolioItem)
			=> InvokeWithResult(x => x.AddFondToPortfolio(portfolioItem));
	}
}
