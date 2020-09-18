using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Identity;
using MooMed.AspNetCore.Identity.DataTypes;
using MooMed.AspNetCore.Identity.Extension;
using MooMed.Common.Definitions;
using MooMed.Common.Definitions.Eventing.User;
using MooMed.Common.Definitions.Logging;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.Definitions.Models.User.ErrorCodes;
using MooMed.Common.ServiceBase.ServiceBase;
using MooMed.Module.Accounts.Datatypes.Entity;
using MooMed.Module.Accounts.Events.Interface;
using MooMed.Module.Accounts.Helper.Interface;
using MooMed.Module.Accounts.Repository.Converters;
using MooMed.Module.AccountValidation.Service.Interface;

namespace MooMed.Module.AccountValidation.Service
{
	internal class EmailValidationService : MooMedServiceBaseWithLogger, IEmailValidationService
	{
		private readonly IAccountValidationEmailHelper _accountValidationEmailHelper;

		private readonly UserManager<AccountEntity> _userManager;

		private readonly AccountDbConverter _accountDbConverter;

		private readonly IAccountValidationTokenHelper _accountValidationTokenHelper;

		public EmailValidationService(
			IMooMedLogger logger,
			IAccountEventHub accountEventHub,
			IAccountValidationEmailHelper accountValidationEmailHelper,
			UserManager<AccountEntity> userManager,
			AccountDbConverter accountDbConverter,
			IAccountValidationTokenHelper accountValidationTokenHelper)
			: base(logger)
		{
			_accountValidationEmailHelper = accountValidationEmailHelper;
			_userManager = userManager;
			_accountDbConverter = accountDbConverter;
			_accountValidationTokenHelper = accountValidationTokenHelper;

			RegisterEventHandler(accountEventHub.AccountRegistered, OnAccountRegistered);
		}

		private async Task OnAccountRegistered(AccountRegisteredEvent eventArgs)
		{
			var account = eventArgs.Account;

			var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(_accountDbConverter.ToEntity(account));

			var emailTokenEncoded = _accountValidationTokenHelper.Serialize(emailToken);

			await _accountValidationEmailHelper.SendAccountValidationEmail(Language.en, account.Email, account.Id, emailTokenEncoded);
		}

		public async Task<IdentityErrorCode> ValidateAccount(AccountValidationModel accountValidationModel)
		{
			var account = await _userManager.FindByIdAsync(accountValidationModel.AccountId.ToString());

			if (account == null)
			{
				return IdentityErrorCode.InvalidUserName;
			}

			if (account.EmailConfirmed)
			{
				return IdentityErrorCode.EmailAlreadyConfirmed;
			}

			var deserializedToken = _accountValidationTokenHelper.Deserialize(accountValidationModel.Token);

			var validationResult = await _userManager.ConfirmEmailAsync(account, deserializedToken);

			return validationResult.Succeeded ? IdentityErrorCode.Success : validationResult.FirstErrorOrDefault();
		}
	}
}