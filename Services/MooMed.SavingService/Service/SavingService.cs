using MooMed.Common.Definitions.Models.Saving;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.ServiceBase;
using MooMed.Core.DataTypes;
using MooMed.Logging.Abstractions.Interface;
using MooMed.Module.Saving.Service.Interface;
using MooMed.ServiceBase.Services.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MooMed.SavingService.Service
{
	public class SavingService : MooMedServiceBase, ISavingService
	{
		private readonly ICurrencyService _currencyService;

		private readonly ICashFlowItemService _cashFlowItemService;

		private readonly IAssetService _assetService;

		public SavingService(
			IMooMedLogger logger,
			ICurrencyService currencyService,
			ICashFlowItemService cashFlowItemService,
			IAssetService assetService)
			: base(logger)
		{
			_currencyService = currencyService;
			_cashFlowItemService = cashFlowItemService;
			_assetService = assetService;
		}

		public async Task<ServiceResponse> SetCurrency(SetCurrencyModel setCurrencyModel)
		{
			await _currencyService.SetCurrency(setCurrencyModel.SessionContext, setCurrencyModel.Currency);
			return ServiceResponse.Success();
		}

		public async Task<ServiceResponse> SetAssets(AssetsModel assetsModel)
		{
			await _assetService.SetAssets(assetsModel.SessionContext, assetsModel);
			return ServiceResponse.Success();
		}

		public async Task<ServiceResponse> SetCashFlowItems(SetCashFlowItemsModel setCashFlowItemsModel)
		{
			await _cashFlowItemService.SaveCashFlowItems(setCashFlowItemsModel.SessionContext, setCashFlowItemsModel.CashFlowItems);
			return ServiceResponse.Success();
		}

		public async Task<ServiceResponse<SavingInfoModel>> GetSavingInformation(ISessionContext sessionContext)
		{
			var currencyRetrievalTask = _currencyService.GetCurrency(sessionContext);
			var cashFlowItemsTask = _cashFlowItemService.GetCashFlowItems(sessionContext);

			await Task.WhenAll(currencyRetrievalTask, cashFlowItemsTask);

			var savingModel = new SavingInfoModel()
			{
				Currency = currencyRetrievalTask.Result,
			};

			// First, order all items by type
			var cashFlowItems = cashFlowItemsTask.Result
				.GroupBy(x => x.CashFlowItemType, x => x)
				.ToDictionary(x => x.Key, x => x.Select(y => y));

			if (cashFlowItems.Any())
			{
				// Setup the special items
				savingModel.BasicSavingInfo = new BasicSavingInfoModel()
				{
					Groceries = cashFlowItems.GetValueOrDefault(CashFlowItemType.Groceries)?.First(),
					Income = cashFlowItems.GetValueOrDefault(CashFlowItemType.Income)?.First(),
					Rent = cashFlowItems.GetValueOrDefault(CashFlowItemType.Rent)?.First(),
				};

				if (cashFlowItems.ContainsKey(CashFlowItemType.Unspecified))
				{
					savingModel.FreeFormSavingInfo = cashFlowItems[CashFlowItemType.Unspecified].ToList();
				}
			}

			return ServiceResponse.Success(savingModel);
		}
	}
}