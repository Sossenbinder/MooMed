using System.Collections.Generic;
using System.Fabric;
using System.Threading.Tasks;
using JetBrains.Annotations;
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
    /// <summary>
    /// The FabricRuntime creates an instance of this class for each service type instance. 
    /// </summary>
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
            [NotNull] IAccountValidationTokenHelper accountValidationTokenHelper,
            [NotNull] IAccountEventHub accountEventHub)
            : base(logger)
        {
            m_accountValidationDataHelper = accountValidationDataHelper;
            m_accountValidationEmailHelper = accountValidationEmailHelper;
            m_accountValidationTokenHelper = accountValidationTokenHelper;

            accountEventHub.AccountRegistered.Register(OnAccountRegistered);
        }

        private async Task OnAccountRegistered((Account, Language) args)
        {
            var (account, lang) = args;

            var accountEmailValidationInfo = (await m_accountValidationDataHelper.CreateEmailValidationKey(account.Id)).ToModel();

            var accountValidationEmailToken = m_accountValidationTokenHelper.Serialize(new AccountValidationTokenData()
            {
                AccountId = account.Id,
                ValidationGuid = accountEmailValidationInfo.ValidationGuid
            });

            await m_accountValidationEmailHelper.SendAccountValidationEmail(lang, account.Email, accountValidationEmailToken);
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
        public async Task<WorkerResponse<bool>> ValidateRegistration(AccountValidationTokenData tokenData)
        {
            var resultCode = await m_accountValidationDataHelper.CheckAndUpdateValidation(tokenData);

            if (resultCode == AccountValidationResult.Success || resultCode == AccountValidationResult.AlreadyValidated)
            {
                // Validation went smoothly or already happened, we can get rid of the validation entry for this account
                await m_accountValidationDataHelper.DeleteValidationDetails(tokenData.AccountId);
            }

            if (resultCode == AccountValidationResult.Success)
            {
                return WorkerResponse<bool>.Success(true);
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

            return WorkerResponse<bool>.Failure(errorMessage: errorMessage);
        }
    }
}
