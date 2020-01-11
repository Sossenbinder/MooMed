using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.Definitions.Models.User.ErrorCodes;
using MooMed.Core.Code.Helper.Crypto;
using MooMed.Core.Code.Logging.Loggers.Interface;
using MooMed.Core.DataTypes;
using MooMed.Module.Accounts.Helper.Interface;
using MooMed.Module.Accounts.Repository;

namespace MooMed.Module.Accounts.Helper
{
    public class AccountSignInService : IAccountSignInService
    {
        [NotNull]
        private readonly IAccountSignInValidator m_accountSignInValidator;

        [NotNull]
        private readonly IMainLogger m_mainLogger;

        [NotNull]
        private readonly AccountDataRepository m_accountDataRepository;

        public AccountSignInService(
            [NotNull] IAccountSignInValidator accountSignInValidator,
            [NotNull] IMainLogger mainLogger,
            [NotNull] AccountDataRepository accountDataRepository)
        {
            m_accountSignInValidator = accountSignInValidator;
            m_accountDataRepository = accountDataRepository;
            m_mainLogger = mainLogger;
        }

        [ItemNotNull]
        public async Task<RegistrationResult> Register(RegisterModel registerModel)
        {
            // Check if given params already make account creation impossible
            var registrationValidationResult = m_accountSignInValidator.ValidateRegistrationModel(registerModel);

            if (registrationValidationResult != RegistrationValidationResult.Success)
            {
                return RegistrationResult.Failure(registrationValidationResult);
            }

            // Check if the database already has entries for given email or username
            if (await m_accountDataRepository.FindAccount(acc => acc.Email.Equals(registerModel.Email)) != null)
            {
                return RegistrationResult.Failure(RegistrationValidationResult.EmailTaken);
            }

            if (await m_accountDataRepository.FindAccount(acc => acc.UserName.Equals(registerModel.UserName)) != null)
            {
                return RegistrationResult.Failure(RegistrationValidationResult.UserNameTaken);
            }

            // Actually Create the account
            var account = (await m_accountDataRepository.CreateAccount(registerModel)).ToModel();

            return RegistrationResult.Success(account);
        }

        [ItemNotNull]
        public async Task<ServiceResponse<LoginResult>> Login(LoginModel loginModel)
        {
			// Validate the login data we got
            var loginValidationResult = m_accountSignInValidator.ValidateLoginModel(loginModel);

            if (loginValidationResult != LoginResponseCode.Success)
            {
                return ServiceResponse<LoginResult>.Failure(new LoginResult(loginValidationResult, null));
            }

            string hashedPassword;
            try
            {
                hashedPassword = Sha256Helper.Hash(loginModel.Password);
            }
            catch (ArgumentException)
            {
                return ServiceResponse<LoginResult>.Failure(new LoginResult(loginValidationResult, null));
            }

            var account = (await m_accountDataRepository.FindAccount(accDbModel => accDbModel.Email.Equals(loginModel.Email) 
                                                                               && accDbModel.PasswordHash.Equals(hashedPassword)))?.ToModel();

            if (account == null)
			{
				return ServiceResponse<LoginResult>.Failure(new LoginResult(LoginResponseCode.AccountNotFound, null));
			}

            if (!account.EmailValidated)
            {
	            return ServiceResponse<LoginResult>.Failure(new LoginResult(LoginResponseCode.EmailNotValidated, null));
            }

            return ServiceResponse<LoginResult>.Success(new LoginResult(LoginResponseCode.Success, account));
        }

        public async Task<bool> RefreshLastAccessed(ISessionContext sessionContext)
        {
            m_mainLogger.Info("Refreshing login for account", sessionContext);
            return await m_accountDataRepository.RefreshLastAccessedAt(sessionContext);
        }
    }
}
