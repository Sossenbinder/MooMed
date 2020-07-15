using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.Definitions.Models.User.ErrorCodes;
using MooMed.Core.DataTypes;
using MooMed.Encryption;
using MooMed.Logging.Loggers.Interface;
using MooMed.Module.Accounts.Helper.Interface;
using MooMed.Module.Accounts.Repository.Interface;
using MooMed.Module.Accounts.Service.Interface;

namespace MooMed.Module.Accounts.Service
{
	internal class LoginService : ILoginService
	{
		[NotNull]
		private readonly IAccountSignInValidator _accountSignInValidator;

		[NotNull]
		private readonly IMooMedLogger _logger;

		[NotNull]
		private readonly IAccountRepository _accountRepository;

		public LoginService(
			[NotNull] IAccountSignInValidator accountSignInValidator,
			[NotNull] IMooMedLogger logger,
			[NotNull] IAccountRepository accountRepository)
		{
			_accountSignInValidator = accountSignInValidator;
			_logger = logger;
			_accountRepository = accountRepository;
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
			_logger.Info("Refreshing login for account", sessionContext);
			return await _accountRepository.RefreshLastAccessedAt(sessionContext);
		}
    }
}
