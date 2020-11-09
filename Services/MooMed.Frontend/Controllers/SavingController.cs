using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MooMed.Common.Definitions.Models.Saving;
using MooMed.Frontend.Controllers.Base;
using MooMed.Frontend.Controllers.Result;
using MooMed.Module.Saving.DataTypes.UiModels;
using MooMed.ServiceBase.Services.Interface;

namespace MooMed.Frontend.Controllers
{
	//[ApiController]
	//[Route("[controller]")]
	public class SavingController : SessionBaseController
	{
		private readonly ISavingService _savingService;

		public SavingController(
			[NotNull] ISessionService sessionService,
			[NotNull] ISavingService savingService)
			: base(sessionService)
		{
			_savingService = savingService;
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
	}
}