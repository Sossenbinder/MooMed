using System;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.Definitions.Models.User.ErrorCodes;
using MooMed.Common.ServiceBase.ServiceBase;
using MooMed.Core.DataTypes;
using MooMed.Core.Translations.Resources;
using MooMed.Grpc.Services.Interface;
using MooMed.Logging.Loggers.Interface;
using MooMed.Module.Accounts.Helper.Interface;
using MooMed.Module.Accounts.Repository.Interface;
using MooMed.Module.AccountValidation.Service.Interface;

namespace MooMed.Stateful.AccountValidationService.Service
{
	public class AccountValidationService : MooMedServiceBaseWithLogger, IAccountValidationService
	{
		[NotNull]
		private readonly IAccountValidationRepository _accountValidationRepository;

		[NotNull]
		private readonly IAccountValidationTokenHelper _accountValidationTokenHelper;

		[NotNull]
		private readonly IEmailValidationService _emailValidationService;

		public AccountValidationService(
			[NotNull] IMooMedLogger logger,
			[NotNull] IAccountValidationRepository accountValidationRepository,
			[NotNull] IAccountValidationTokenHelper accountValidationTokenHelper,
			[NotNull] IEmailValidationService emailValidationService)
			: base(logger)
		{
			_accountValidationRepository = accountValidationRepository;
			_accountValidationTokenHelper = accountValidationTokenHelper;
			_emailValidationService = emailValidationService;
		}

		/// <summary>
		/// Takes care of the validation of an account
		/// </summary>
		/// <param name="accountValidationModel">Object containing accountId and token</param>
		/// <returns></returns>
		[ItemNotNull]
		public async Task<ServiceResponse<bool>> ValidateRegistration(AccountValidationModel accountValidationModel)
		{
			await _emailValidationService.ValidateAccount(accountValidationModel);
			var resultCode = AccountValidationResult.None;

			if (resultCode == AccountValidationResult.Success)
			{
				var validationEntity = await _accountValidationRepository.Read(x => x.Id == accountValidationModel.AccountId);

				// Validation went smoothly or already happened, we can get rid of the validation entry for this account
				await _accountValidationRepository.Delete(validationEntity.First());

				return ServiceResponse<bool>.Success(true);
			}

			string errorMessage = null;

			switch (resultCode)
			{
				case AccountValidationResult.AlreadyValidated:
					errorMessage = Translation.AccountValidationAlreadyValidated;
					break;

				case AccountValidationResult.ValidationGuidInvalid:
					errorMessage = Translation.AccountValidationInvalidGuid;
					break;
			}

			return ServiceResponse<bool>.Failure(errorMessage: errorMessage);
		}
	}
}