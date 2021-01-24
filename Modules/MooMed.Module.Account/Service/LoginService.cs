using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.Definitions.Models.User.ErrorCodes;
using MooMed.Common.ServiceBase.ServiceBase;
using MooMed.Logging.Abstractions.Interface;
using MooMed.Module.Accounts.Datatypes.Entity;
using MooMed.Module.Accounts.Events.Interface;
using MooMed.Module.Accounts.Helper.Interface;
using MooMed.Module.Accounts.Repository.Converters;
using MooMed.Module.Accounts.Repository.Interface;
using MooMed.Module.Accounts.Service.Interface;

namespace MooMed.Module.Accounts.Service
{
	internal class LoginService : Common.ServiceBase.ServiceBase.ServiceBase, ILoginService
	{
		[NotNull]
		private readonly ILogonModelValidator _logonModelValidator;

		[NotNull]
		private readonly IMooMedLogger _logger;

		[NotNull]
		private readonly IAccountRepository _accountRepository;

		[NotNull]
		private readonly SignInManager<AccountEntity> _signInManager;

		[NotNull]
		private readonly UserManager<AccountEntity> _userManager;

		[NotNull]
		private readonly AccountDbConverter _accountDbConverter;

		public LoginService(
			[NotNull] ILogonModelValidator logonModelValidator,
			[NotNull] IMooMedLogger logger,
			[NotNull] IAccountRepository accountRepository,
			[NotNull] IAccountEventHub accountEventHub,
			[NotNull] SignInManager<AccountEntity> signInManager,
			[NotNull] UserManager<AccountEntity> userManager,
			[NotNull] AccountDbConverter accountDbConverter)
		{
			_logonModelValidator = logonModelValidator;
			_logger = logger;
			_accountRepository = accountRepository;
			_signInManager = signInManager;
			_userManager = userManager;
			_accountDbConverter = accountDbConverter;

			RegisterEventHandler(accountEventHub.AccountLoggedIn, args => RefreshLastAccessed(args.SessionContext));
			RegisterEventHandler(accountEventHub.AccountLoggedOut, args => RefreshLastAccessed(args.SessionContext));
		}

		public async Task<LoginResult> Login(LoginModel loginModel)
		{
			// Validate the login data we got
			var loginValidationResult = _logonModelValidator.ValidateLoginModel(loginModel);

			if (loginValidationResult != IdentityErrorCode.Success)
			{
				return new LoginResult(loginValidationResult);
			}

			var account = await _userManager.FindByEmailAsync(loginModel.Email);

			if (account == null)
			{
				return new LoginResult(IdentityErrorCode.InvalidEmail);
			}

			var validator = await _userManager.CheckPasswordAsync(account, loginModel.Password);

			if (validator == false)
			{
				return new LoginResult(IdentityErrorCode.PasswordMismatch);
			}

			var result = await _signInManager.PasswordSignInAsync(account.UserName, loginModel.Password, loginModel.RememberMe, false);

			if (result.Succeeded)
			{
				return new LoginResult(IdentityErrorCode.Success, _accountDbConverter.ToModel(account));
			}

			if (!account.EmailConfirmed)
			{
				return new LoginResult(IdentityErrorCode.EmailNotConfirmed);
			}

			return new LoginResult(IdentityErrorCode.DefaultError);
		}

		public Task<bool> RefreshLastAccessed(ISessionContext sessionContext)
		{
			_logger.Info("Refreshing login for account", sessionContext);
			return _accountRepository.RefreshLastAccessedAt(sessionContext);
		}
	}
}