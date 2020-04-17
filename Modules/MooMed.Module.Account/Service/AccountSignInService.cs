using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Database.Converter;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.Definitions.Models.User.ErrorCodes;
using MooMed.Core.Code.Helper.Crypto;
using MooMed.Core.Code.Logging.Loggers.Interface;
using MooMed.Core.DataTypes;
using MooMed.Module.Accounts.Datatypes.Entity;
using MooMed.Module.Accounts.Helper.Interface;
using MooMed.Module.Accounts.Repository;
using MooMed.Module.Accounts.Repository.Interface;
using MooMed.Module.Accounts.Service.Interface;

namespace MooMed.Module.Accounts.Service
{
    public class AccountSignInService : IAccountSignInService
    {
        [NotNull]
        private readonly IAccountSignInValidator _accountSignInValidator;

        [NotNull]
        private readonly IMainLogger _mainLogger;

        [NotNull]
        private readonly IAccountRepository _accountRepository;

        [NotNull]
        private readonly IModelConverter<Account, AccountEntity, int> _accountModelConverter;

        public AccountSignInService(
            [NotNull] IAccountSignInValidator accountSignInValidator,
            [NotNull] IMainLogger mainLogger,
            [NotNull] IAccountRepository accountRepository, 
            [NotNull] IModelConverter<Account, AccountEntity, int> accountModelConverter)
        {
            _accountSignInValidator = accountSignInValidator;
            _accountRepository = accountRepository;
            _accountModelConverter = accountModelConverter;
            _mainLogger = mainLogger;
        }

        [ItemNotNull]
        public async Task<RegistrationResult> Register(RegisterModel registerModel)
        {
            // Check if given params already make account creation impossible
            var registrationValidationResult = _accountSignInValidator.ValidateRegistrationModel(registerModel);

            if (registrationValidationResult != RegistrationValidationResult.Success)
            {
                return RegistrationResult.Failure(registrationValidationResult);
            }

            // Check if the database already has entries for given email or username
            if (await _accountRepository.FindAccount(acc => acc.Email.Equals(registerModel.Email)) != null)
            {
                return RegistrationResult.Failure(RegistrationValidationResult.EmailTaken);
            }

            if (await _accountRepository.FindAccount(acc => acc.UserName.Equals(registerModel.UserName)) != null)
            {
                return RegistrationResult.Failure(RegistrationValidationResult.UserNameTaken);
            }

            // Actually Create the account
            var account = _accountModelConverter.ToModel(await _accountRepository.CreateAccount(registerModel));
            
            return RegistrationResult.Success(account);
        }

        [ItemNotNull]
        public async Task<ServiceResponse<LoginResult>> Login(LoginModel loginModel)
        {
			// Validate the login data we got
            var loginValidationResult = _accountSignInValidator.ValidateLoginModel(loginModel);

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

            var account = (await _accountRepository.FindAccount(accDbModel => accDbModel.Email.Equals(loginModel.Email) 
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
            _mainLogger.Info("Refreshing login for account", sessionContext);
            return await _accountRepository.RefreshLastAccessedAt(sessionContext);
        }
    }
}
