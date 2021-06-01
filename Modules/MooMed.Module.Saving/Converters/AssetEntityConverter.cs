using MooMed.Common.Database.Converter;
using MooMed.Common.Definitions.Models.Saving;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Module.Saving.Database.Entities;

namespace MooMed.Module.Saving.Converters
{
	public class AssetEntityConverter : IBiDirectionalDbConverter<AssetsModel, AssetsEntity, int>
	{
		public AssetsEntity ToEntity(AssetsModel model, ISessionContext sessionContext = null!)
		{
			return new()
			{
				Id = model.SessionContext.Account.Id,
				Cash = model.Cash,
				Commodities = model.Commodities,
				Debt = model.Debt,
				Equity = model.Equity,
				Estate = model.Estate,
			};
		}

		public AssetsModel ToModel(AssetsEntity entity)
		{
			return new()
			{
				Cash = entity.Cash,
				Commodities = entity.Commodities,
				Debt = entity.Debt,
				Equity = entity.Equity,
				Estate = entity.Estate,
			};
		}
	}
}