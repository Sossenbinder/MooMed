using System.Threading.Tasks;
using MooMed.Common.Definitions.Logging;
using MooMed.Common.Definitions.Models.Saving;
using MooMed.Common.ServiceBase.ServiceBase;
using MooMed.Core.DataTypes;
using MooMed.Module.Saving.Service.Interface;
using MooMed.ServiceBase.Services.Interface;

namespace MooMed.SavingService.Service
{
	public class SavingService : MooMedServiceBaseWithLogger, ISavingService
	{
		private readonly ICurrencyService _currencyService;

		public SavingService(
			IMooMedLogger logger,
			ICurrencyService currencyService)
			: base(logger)
		{
			_currencyService = currencyService;
		}

		public async Task<ServiceResponse> SetCurrency(SetCurrencyModel setCurrencyModel)
		{
			await _currencyService.SetCurrency(setCurrencyModel.SessionContext, setCurrencyModel.Currency);
			return ServiceResponse.Success();
		}
	}
}