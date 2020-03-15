using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MooMed.Common.Definitions.Models.User.ErrorCodes;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Web.Controllers.Base;
using MooMed.Web.Controllers.Result;
using MooMed.Web.Models;
using Newtonsoft.Json;

namespace MooMed.Web.Controllers
{
    public class AccountValidationController : BaseController
    {
	    [NotNull]
	    private readonly IAccountService m_accountService;

	    [NotNull]
	    private readonly IAccountValidationService m_accountValidationService;

	    public AccountValidationController(
		    [NotNull] IAccountService accountService,
		    [NotNull] IAccountValidationService accountValidationService)
	    {
		    m_accountService = accountService;
		    m_accountValidationService = accountValidationService;
	    }

        /// <summary>
        /// Return the base validation page where user is provided some information
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<ActionResult> Index([CanBeNull] string token)
        {
            string jsonString;

            if (token != null)
            {
                var deserializedToken = await m_accountValidationService.DeserializeRawToken(token);
                var account = await m_accountService.FindById(deserializedToken.AccountId);

                if (account.IsFailure)
                {
                    jsonString = JsonConvert.SerializeObject(new
                    {
                        AccountValidationResult = AccountValidationResult.AccountNotFound
                    });
                }
                else
                {
                    var accountValidationModel = new AccountValidationModel
                    {
                        AccountName = account.PayloadOrFail.UserName,
                        AccountValidationTokenData = deserializedToken
                    };
                    jsonString = JsonConvert.SerializeObject(accountValidationModel); 
                }
            }
            else
            {
                jsonString = JsonConvert.SerializeObject(new
                {
                    AccountValidationResult = AccountValidationResult.TokenInvalid
                });
            }

            return View("~/Views/Other/Other.cshtml", new ControllerMetaData("MooMed - AccountValidation", CurrentUiLanguage, jsonString));
        }

        /// <summary>
        /// Validate registration of an account
        /// </summary>
        /// <param name="accountValidationModel"></param>
        /// <returns></returns>
        [ItemNotNull]
        [AllowAnonymous]
        public async Task<JsonResponse> ValidateRegistration([NotNull] AccountValidationModel accountValidationModel)
        {
            var result = await m_accountValidationService.ValidateRegistration(accountValidationModel.AccountValidationTokenData);

            return result.ToJsonResponse();
        }
    }
}