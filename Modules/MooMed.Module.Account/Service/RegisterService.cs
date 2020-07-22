using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using MooMed.Common.Database.Converter;
using MooMed.Common.Definitions.Models.User;
using MooMed.Logging.Loggers.Interface;
using MooMed.Module.Accounts.Datatypes.Entity;
using MooMed.Module.Accounts.Helper.Interface;
using MooMed.Module.Accounts.Repository.Interface;
using MooMed.Module.Accounts.Service.Interface;

namespace MooMed.Module.Accounts.Service
{
	internal class RegisterService : IRegisterService
	{
		[NotNull]
		private readonly IAccountSignInValidator _accountSignInValidator;

		[NotNull]
		private readonly IMooMedLogger _logger;

		[NotNull]
		private readonly IAccountRepository _accountRepository;

		[NotNull]
		private readonly IEntityToModelConverter<AccountEntity, Account, int> _accountEntityToModelConverter;

		[NotNull]
		private readonly IModelToEntityConverter<RegisterModel, AccountEntity, int> _registerModelToAccountConverter;

		[NotNull]
		private readonly UserManager<Account> _userManager;

		public RegisterService(
			[NotNull] IAccountSignInValidator accountSignInValidator,
			[NotNull] IMooMedLogger logger,
			[NotNull] IAccountRepository accountRepository,
			[NotNull] IEntityToModelConverter<AccountEntity, Account, int> accountEntityToModelConverter,
			[NotNull] IModelToEntityConverter<RegisterModel, AccountEntity, int> registerModelToAccountConverter,
			[NotNull] UserManager<Account> userManager)
		{
			_accountSignInValidator = accountSignInValidator;
			_logger = logger;
			_accountRepository = accountRepository;
			_accountEntityToModelConverter = accountEntityToModelConverter;
			_registerModelToAccountConverter = registerModelToAccountConverter;
			_userManager = userManager;
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
			if ((await _accountRepository.Read(acc => acc.Email.Equals(registerModel.Email))).SingleOrDefault() != null)
			{
				return RegistrationResult.Failure(RegistrationValidationResult.EmailTaken);
			}

			if ((await _accountRepository.Read(acc => acc.UserName.Equals(registerModel.UserName))).SingleOrDefault() != null)
			{
				return RegistrationResult.Failure(RegistrationValidationResult.UserNameTaken);
			}

			var accountEntity = _registerModelToAccountConverter.ToEntity(registerModel);

			var result = await _userManager.CreateAsync(new Account()
			{
				Email = registerModel.Email,
				UserName = registerModel.UserName,
			}, registerModel.Password);

			// Actually Create the account
			var account = _accountEntityToModelConverter.ToModel(await _accountRepository.Create(accountEntity));

			_logger.Info($"Registered {registerModel.Email} as accountId {account.Id}");

			return RegistrationResult.Success(account);
		}
    }
}
