using System;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Database.Converter;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.Definitions.Models.User.ErrorCodes;
using MooMed.Common.ServiceBase;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Core.Code.Logging.Loggers.Interface;
using MooMed.Core.DataTypes;
using MooMed.Core.Translations.Resources;
using MooMed.Module.Accounts.Datatypes.Entity;
using MooMed.Module.Accounts.Helper.Interface;
using MooMed.Module.Accounts.Repository;
using MooMed.Module.Accounts.Repository.Interface;

namespace MooMed.Stateful.AccountValidationService.Service
{
    public class AccountValidationService : MooMedServiceBase, IAccountValidationService
    {
        [NotNull]
        private readonly IAccountValidationRepository _accountValidationRepository;

        [NotNull]
        private readonly IAccountValidationTokenHelper _accountValidationTokenHelper;

        [NotNull]
        private readonly IBiDirectionalDbConverter<AccountValidation, AccountValidationEntity, int> _accountValidationConverter;

        [NotNull]
        private readonly IAccountValidationEmailHelper _accountValidationEmailHelper;
        
        public AccountValidationService(
            [NotNull] IMainLogger logger,
            [NotNull] IAccountValidationRepository accountValidationRepository,
            [NotNull] IAccountValidationEmailHelper accountValidationEmailHelper,
            [NotNull] IAccountValidationTokenHelper accountValidationTokenHelper,
            [NotNull] IBiDirectionalDbConverter<AccountValidation, AccountValidationEntity, int> accountValidationConverter)
            : base(logger)
        {
            _accountValidationRepository = accountValidationRepository;
            _accountValidationEmailHelper = accountValidationEmailHelper;
            _accountValidationTokenHelper = accountValidationTokenHelper;
            _accountValidationConverter = accountValidationConverter;
        }

        public async Task SendAccountValidationMail(AccountValidationMailData accountValidationMailData)
        {
	        var account = accountValidationMailData.Account;

	        var accountValidation = new AccountValidation()
	        {
		        AccountId = account.Id,
		        ValidationGuid = Guid.NewGuid()
	        };

            await _accountValidationRepository.Create(_accountValidationConverter.ToEntity(accountValidation));
            
            var accountValidationEmailToken = _accountValidationTokenHelper.Serialize(new AccountValidationTokenData()
            {
                AccountId = account.Id,
                ValidationGuid = accountValidation.ValidationGuid
            });

            await _accountValidationEmailHelper.SendAccountValidationEmail(accountValidationMailData.Language, account.Email, accountValidationEmailToken);
        }

        public Task<AccountValidationTokenData> DeserializeRawToken(string token)
        {
            return Task.FromResult(_accountValidationTokenHelper.Deserialize(token));
        }

        /// <summary>
        /// Takes care of the validation of an account
        /// </summary>
        /// <param name="tokenData">Object containing accountId and token</param>
        /// <returns></returns>
        [ItemNotNull]
        public async Task<ServiceResponse<bool>> ValidateRegistration(AccountValidationTokenData tokenData)
        {
            var resultCode = await _accountValidationRepository.CheckAndUpdateValidation(tokenData);

            if (resultCode == AccountValidationResult.Success)
            {
	            var validationEntity = await _accountValidationRepository.Read(x => x.Id == tokenData.AccountId);

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
