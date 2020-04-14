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
        private readonly IAccountValidationRepository m_accountValidationRepository;

        [NotNull]
        private readonly IAccountValidationTokenHelper m_accountValidationTokenHelper;

        [NotNull]
        private readonly IBiDirectionalDbConverter<AccountValidation, AccountValidationEntity, int> m_accountValidationConverter;

        [NotNull]
        private readonly IAccountValidationEmailHelper m_accountValidationEmailHelper;
        
        public AccountValidationService(
            [NotNull] IMainLogger logger,
            [NotNull] IAccountValidationRepository accountValidationRepository,
            [NotNull] IAccountValidationEmailHelper accountValidationEmailHelper,
            [NotNull] IAccountValidationTokenHelper accountValidationTokenHelper,
            [NotNull] IBiDirectionalDbConverter<AccountValidation, AccountValidationEntity, int> accountValidationConverter)
            : base(logger)
        {
            m_accountValidationRepository = accountValidationRepository;
            m_accountValidationEmailHelper = accountValidationEmailHelper;
            m_accountValidationTokenHelper = accountValidationTokenHelper;
            m_accountValidationConverter = accountValidationConverter;
        }

        public async Task SendAccountValidationMail(AccountValidationMailData accountValidationMailData)
        {
	        var account = accountValidationMailData.Account;

	        var accountValidation = new AccountValidation()
	        {
		        AccountId = account.Id,
		        ValidationGuid = Guid.NewGuid()
	        };

            await m_accountValidationRepository.Create(m_accountValidationConverter.ToEntity(accountValidation));
            
            var accountValidationEmailToken = m_accountValidationTokenHelper.Serialize(new AccountValidationTokenData()
            {
                AccountId = account.Id,
                ValidationGuid = accountValidation.ValidationGuid
            });

            await m_accountValidationEmailHelper.SendAccountValidationEmail(accountValidationMailData.Language, account.Email, accountValidationEmailToken);
        }

        public Task<AccountValidationTokenData> DeserializeRawToken(string token)
        {
            return Task.FromResult(m_accountValidationTokenHelper.Deserialize(token));
        }

        /// <summary>
        /// Takes care of the validation of an account
        /// </summary>
        /// <param name="tokenData">Object containing accountId and token</param>
        /// <returns></returns>
        [ItemNotNull]
        public async Task<ServiceResponse<bool>> ValidateRegistration(AccountValidationTokenData tokenData)
        {
            var resultCode = await m_accountValidationRepository.CheckAndUpdateValidation(tokenData);

            if (resultCode == AccountValidationResult.Success)
            {
	            var validationEntity = await m_accountValidationRepository.Read(x => x.Id == tokenData.AccountId);

                // Validation went smoothly or already happened, we can get rid of the validation entry for this account
                await m_accountValidationRepository.Delete(validationEntity.First());

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
