using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
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

		[NotNull]
		private readonly SignInManager<Account> _signInManager;

		[NotNull]
		private readonly UserManager<Account> _userManager;

		public LoginService(
			[NotNull] IAccountSignInValidator accountSignInValidator,
			[NotNull] IMooMedLogger logger,
			[NotNull] IAccountRepository accountRepository,
			[NotNull] SignInManager<Account> signInManager,
			[NotNull] UserManager<Account> userManager)
		{
			_accountSignInValidator = accountSignInValidator;
			_logger = logger;
			_accountRepository = accountRepository;
			_signInManager = signInManager;
			_userManager = userManager;
		}

		[ItemNotNull]
		public async Task<LoginResult> Login(LoginModel loginModel)
		{
			// Validate the login data we got
			var loginValidationResult = _accountSignInValidator.ValidateLoginModel(loginModel);

			if (loginValidationResult != LoginResponseCode.Success)
			{
				return new LoginResult(loginValidationResult);
			}

			var account = await _userManager.FindByEmailAsync(loginModel.Email);

			if (account == null)
			{
				return new LoginResult(LoginResponseCode.AccountNotFound);
			}

			var validator = await _userManager.CheckPasswordAsync(account, loginModel.Password);

			if (validator == false)
			{
				return new LoginResult(LoginResponseCode.PasswordWrong);
			}

			var result = await _signInManager.PasswordSignInAsync(account.UserName, loginModel.Password, loginModel.RememberMe, false);

			if (result.Succeeded)
			{
				return new LoginResult(LoginResponseCode.Success, account);
			}

			if (!account.EmailConfirmed)
			{
				return new LoginResult(LoginResponseCode.EmailNotValidated);
			}

			return new LoginResult(LoginResponseCode.UnknownFailure);
		}

		public Task<bool> RefreshLastAccessed(ISessionContext sessionContext)
		{
			_logger.Info("Refreshing login for account", sessionContext);
			return _accountRepository.RefreshLastAccessedAt(sessionContext);
		}
    }
}
