using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MooMed.Common.Definitions.Logging;
using MooMed.Common.Definitions.Models.Saving;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.ServiceBase.ServiceBase;
using MooMed.Core.DataTypes;
using MooMed.Module.Saving.Service.Interface;
using MooMed.ServiceBase.Services.Interface;

namespace MooMed.SavingService.Service
{
    public class SavingService : MooMedServiceBaseWithLogger, ISavingService
    {
        private readonly ICurrencyService _currencyService;

        private readonly ICashFlowItemService _cashFlowItemService;

        public SavingService(
            IMooMedLogger logger,
            ICurrencyService currencyService,
            ICashFlowItemService cashFlowItemService)
            : base(logger)
        {
            _currencyService = currencyService;
            _cashFlowItemService = cashFlowItemService;
        }

        public async Task<ServiceResponse> SetCurrency(SetCurrencyModel setCurrencyModel)
        {
            await _currencyService.SetCurrency(setCurrencyModel.SessionContext, setCurrencyModel.Currency);
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

            var cashFlowItems = cashFlowItemsTask.Result
                .ToDictionary(x => x.CashFlowItemType, x => x);

            if (cashFlowItems.Any())
            {
                savingModel.BasicSavingInfo = new BasicSavingInfoModel()
                {
                    Groceries = cashFlowItems.GetValueOrDefault(CashFlowItemType.Groceries)!,
                    Income = cashFlowItems.GetValueOrDefault(CashFlowItemType.Income)!,
                    Rent = cashFlowItems.GetValueOrDefault(CashFlowItemType.Rent)!,
                };
            }

            return ServiceResponse.Success(savingModel);
        }
    }
}