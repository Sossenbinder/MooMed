using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions;
using MooMed.Common.Definitions.Database.Entities;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.Definitions.Models.User.ErrorCodes;
using MooMed.Common.ServiceBase;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Core.Code.Logging.Loggers.Interface;
using MooMed.Core.DataTypes;
using MooMed.Core.Translations;
using MooMed.Core.Translations.Resources;
using MooMed.Module.Accounts.Events.Interface;
using MooMed.Module.Accounts.Helper.Interface;
using MooMed.Module.Accounts.Repository;

namespace MooMed.Stateful.AccountValidationService.Service
{
    public class AccountValidationService : MooMedServiceBase, IAccountValidationService
    {
        [NotNull]
        private readonly AccountValidationDataHelper m_accountValidationDataHelper;

        [NotNull]
        private readonly IAccountValidationTokenHelper m_accountValidationTokenHelper;

        [NotNull]
        private readonly IAccountValidationEmailHelper m_accountValidationEmailHelper;

        public AccountValidationService(
            [NotNull] IMainLogger logger,
            [NotNull] AccountValidationDataHelper accountValidationDataHelper,
            [NotNull] IAccountValidationEmailHelper accountValidationEmailHelper,
            [NotNull] IAccountValidationTokenHelper accountValidationTokenHelper)
            : base(logger)
        {
            m_accountValidationDataHelper = accountValidationDataHelper;
            m_accountValidationEmailHelper = accountValidationEmailHelper;
            m_accountValidationTokenHelper = accountValidationTokenHelper;
        }

        public async Task SendAccountValidationMail(AccountValidationMailData accountValidationMailData)
        {
	        var account = accountValidationMailData.Account;

            var accountEmailValidationInfo = (await m_accountValidationDataHelper.CreateEmailValidationKey(account.Id)).ToModel();

            var accountValidationEmailToken = m_accountValidationTokenHelper.Serialize(new AccountValidationTokenData()
            {
                AccountId = account.Id,
                ValidationGuid = accountEmailValidationInfo.ValidationGuid
            });

            await m_accountValidationEmailHelper.SendAccountValidationEmail(accountValidationMailData.Language, account.Email, accountValidationEmailToken);
        }

        [NotNull]
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
            var resultCode = await m_accountValidationDataHelper.CheckAndUpdateValidation(tokenData);

            if (resultCode == AccountValidationResult.Success)
            {
                // Validation went smoothly or already happened, we can get rid of the validation entry for this account
                await m_accountValidationDataHelper.DeleteValidationDetails(tokenData.AccountId);

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
