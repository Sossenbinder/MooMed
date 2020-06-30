using System.Threading.Tasks;
using MooMed.Common.Definitions.Models.Portfolio;
using MooMed.Common.Definitions.Models.Session.Interface;

namespace MooMed.Module.Portfolio.Service.Interface
{
	public interface IPortfolioService
	{
		Task<bool> AddFondToPortfolio(PortfolioItem portfolioItem);

		Task<Common.Definitions.Models.Portfolio.Portfolio> GetPortfolio(ISessionContext sessionContext);
	}
}
