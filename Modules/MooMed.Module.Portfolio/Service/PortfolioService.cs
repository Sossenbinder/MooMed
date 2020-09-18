using System;
using System.Threading.Tasks;
using MooMed.Common.Definitions.Models.Portfolio;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Module.Portfolio.Converter;
using MooMed.Module.Portfolio.Repository.Interface;
using MooMed.Module.Portfolio.Service.Interface;

namespace MooMed.Module.Portfolio.Service
{
	public class PortfolioService : IPortfolioService
	{
		private readonly PortfolioItemConverter _portfolioItemConverter;

		private readonly IPortfolioMappingRepository _portfolioMappingRepository;

		public PortfolioService(
			PortfolioItemConverter portfolioItemConverter,
			IPortfolioMappingRepository portfolioMappingRepository)
		{
			_portfolioItemConverter = portfolioItemConverter;
			_portfolioMappingRepository = portfolioMappingRepository;
		}

		public async Task<bool> AddFondToPortfolio(PortfolioItem portfolioItem)
		{
			var entity = _portfolioItemConverter.ToEntity(portfolioItem);

			try
			{
				await _portfolioMappingRepository.Create(entity);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public async Task<Common.Definitions.Models.Portfolio.Portfolio> GetPortfolio(ISessionContext sessionContext)
		{
			var items = await _portfolioMappingRepository.Read(x => x.Id == sessionContext.Account.Id);

			return new Common.Definitions.Models.Portfolio.Portfolio()
			{
				Items = items.ConvertAll(x => _portfolioItemConverter.ToModel(x)),
			};
		}
	}
}