using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.Definitions.Models.User.ErrorCodes;
using MooMed.Common.ServiceBase.ServiceBase;
using MooMed.Core.DataTypes;
using MooMed.Logging.Abstractions.Interface;
using MooMed.Module.AccountValidation.Service.Interface;
using MooMed.ServiceBase.Services.Interface;

namespace MooMed.AccountValidationService.Service
{
	public class AccountValidationService : ServiceBaseWithLogger, IAccountValidationService
	{
		[NotNull]
		private readonly IEmailValidationService _emailValidationService;

		public AccountValidationService(
			[NotNull] IMooMedLogger logger,
			[NotNull] IEmailValidationService emailValidationService)
			: base(logger)
		{
			_emailValidationService = emailValidationService;
		}

		/// <summary>
		/// Takes care of the validation of an account
		/// </summary>
		/// <param name="accountValidationModel">Object containing accountId and token</param>
		/// <returns></returns>
		public async Task<ServiceResponse<IdentityErrorCode>> ValidateRegistration(AccountValidationModel accountValidationModel)
		{
			var validationResult = await _emailValidationService.ValidateAccount(accountValidationModel);

			return validationResult != IdentityErrorCode.Success
				? ServiceResponse.Success(validationResult)
				: ServiceResponse<IdentityErrorCode>.Failure();
		}
	}
}