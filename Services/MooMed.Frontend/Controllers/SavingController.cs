using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MooMed.Common.Definitions.Models.Saving;
using MooMed.Frontend.Controllers.Base;
using MooMed.Frontend.Controllers.Result;
using MooMed.Module.Saving.Converters;
using MooMed.Module.Saving.DataTypes.UiModels;
using MooMed.ServiceBase.Services.Interface;

namespace MooMed.Frontend.Controllers
{
    public class SavingController : SessionBaseController
    {
        private readonly ISavingService _savingService;

        private readonly SavingModelToUiModelConverter _savingModelToUiModelConverter;

        public SavingController(
            [NotNull] ISessionService sessionService,
            [NotNull] ISavingService savingService,
            SavingModelToUiModelConverter savingModelToUiModelConverter)
            : base(sessionService)
        {
            _savingService = savingService;
            _savingModelToUiModelConverter = savingModelToUiModelConverter;
        }

        [ItemNotNull]
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<JsonResponse> SetCurrency([FromBody] SetCurrencyUiModel setCurrencyModel)
        {
            var response = await _savingService.SetCurrency(new SetCurrencyModel
            {
                Currency = setCurrencyModel.Currency,
                SessionContext = CurrentSessionOrFail
            });

            return response.ToJsonResponse();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<JsonResponse> SetCashFlowItems([FromBody] SetCashFlowItemsUiModel setCashFlowItems)
        {
            var response = await _savingService.SetCashFlowItems(new SetCashFlowItemsModel()
            {
                SessionContext = CurrentSessionOrFail,
                CashFlowItems = setCashFlowItems.CashFlowItems.ConvertAll(x => new CashFlowItem()
                {
                    Amount = x.Amount,
                    CashFlowItemType = x.CashFlowItemType,
                    FlowType = x.FlowType,
                    Identifier = x.Identifier,
                    Name = x.Name,
                })
            });

            return response.ToJsonResponse();
        }

        [HttpGet]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<JsonDataResponse<SavingInfoUiModel>> GetSavingInfo()
        {
            var result = await _savingService.GetSavingInformation(CurrentSessionOrFail);

            return result.ToUiModelJsonResponse(_savingModelToUiModelConverter);
        }
    }
}