using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MooMed.AspNetCore.Identity.Extension;
using MooMed.Common.Database.Converter;
using MooMed.Common.Definitions.Eventing.User;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.Definitions.Models.User.ErrorCodes;
using MooMed.Common.ServiceBase.ServiceBase;
using MooMed.Logging.Abstractions.Interface;
using MooMed.Module.Accounts.Datatypes.Entity;
using MooMed.Module.Accounts.Events.Interface;
using MooMed.Module.Accounts.Helper.Interface;
using MooMed.Module.Accounts.Service.Interface;

namespace MooMed.Module.Accounts.Service
{
	internal class RegisterService : Common.ServiceBase.ServiceBase.ServiceBase, IRegisterService
	{
		[NotNull]
		private readonly IMooMedLogger _logger;

		[NotNull]
		private readonly IAccountEventHub _accountEventHub;

		[NotNull]
		private readonly IEntityToModelConverter<AccountEntity, Account, int> _accountEntityToModelConverter;

		[NotNull]
		private readonly IModelToEntityConverter<RegisterModel, AccountEntity, int> _registerModelToAccountConverter;

		[NotNull]
		private readonly UserManager<AccountEntity> _userManager;

		[NotNull]
		private readonly IRegistrationModelValidator _registrationModelValidator;

		public RegisterService(
			[NotNull] IMooMedLogger logger,
			[NotNull] IAccountEventHub accountEventHub,
			[NotNull] IEntityToModelConverter<AccountEntity, Account, int> accountEntityToModelConverter,
			[NotNull] IModelToEntityConverter<RegisterModel, AccountEntity, int> registerModelToAccountConverter,
			[NotNull] UserManager<AccountEntity> userManager,
			[NotNull] IRegistrationModelValidator registrationModelValidator)
		{
			_logger = logger;
			_accountEventHub = accountEventHub;
			_accountEntityToModelConverter = accountEntityToModelConverter;
			_registerModelToAccountConverter = registerModelToAccountConverter;
			_userManager = userManager;
			_registrationModelValidator = registrationModelValidator;
		}

		public async Task<RegistrationResult> Register(RegisterModel registerModel)
		{
			var validationResult = _registrationModelValidator.ValidateRegistrationModel(registerModel);

			if (validationResult != IdentityErrorCode.Success)
			{
				return RegistrationResult.Failure(validationResult);
			}

			if (await _userManager.Users.AnyAsync(x => x.Email.Equals(registerModel.Email)))
			{
				return RegistrationResult.Failure(IdentityErrorCode.DuplicateEmail);
			}

			var accountEntity = _registerModelToAccountConverter.ToEntity(registerModel);

			var createResult = await _userManager.CreateAsync(accountEntity, registerModel.Password);

			if (createResult.Succeeded)
			{
				var account = _accountEntityToModelConverter.ToModel(accountEntity);

				await _userManager.UpdateSecurityStampAsync(accountEntity);

				_logger.Info($"Registered {registerModel.Email} as accountId {account.Id}");

				_accountEventHub.AccountRegistered.RaiseFireAndForget(new AccountRegisteredEvent(account));

				return RegistrationResult.Success(account);
			}

			var errorCodes = createResult.GetErrorCodes();

			return RegistrationResult.Failure(errorCodes);
		}
	}
}