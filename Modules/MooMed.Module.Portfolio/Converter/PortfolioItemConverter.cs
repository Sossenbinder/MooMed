using MooMed.Common.Database.Converter;
using MooMed.Common.Definitions.Models.Portfolio;
using MooMed.Module.Portfolio.DataTypes.Entity;

namespace MooMed.Module.Portfolio.Converter
{
	public class PortfolioItemConverter : IBiDirectionalDbConverter<PortfolioItem, PortfolioMappingEntity, int>
	{
		public PortfolioMappingEntity ToEntity(PortfolioItem model)
		{
			return new PortfolioMappingEntity()
			{
				Id = model.SessionContext.Account.Id,
				Isin = model.Isin,
				Amount = model.Amount
			};
		}

		public PortfolioItem ToModel(PortfolioMappingEntity entity)
		{
			return new PortfolioItem()
			{
				Isin = entity.Isin,
				Amount = entity.Amount
			};
		}
	}
}