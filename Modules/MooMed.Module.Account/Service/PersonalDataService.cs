using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using MooMed.AspNetCore.Identity.Extension;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.Definitions.Models.User.ErrorCodes;
using MooMed.Core.DataTypes;
using MooMed.Module.Accounts.Datatypes.Entity;
using MooMed.Module.Accounts.Repository.Converters;
using MooMed.Module.Accounts.Service.Interface;

namespace MooMed.Module.Accounts.Service
{
	internal class PersonalDataService : IPersonalDataService
	{
		[NotNull]
		private readonly UserManager<AccountEntity> _userManager;

		private readonly AccountDbConverter _accountDbConverter;

		public PersonalDataService(
			[NotNull] UserManager<AccountEntity> userManager,
			[NotNull] AccountDbConverter accountDbConverter)
		{
			_userManager = userManager;
			_accountDbConverter = accountDbConverter;
		}

		public async Task<ServiceResponse<IdentityErrorCode>> UpdatePersonalData(PersonalData personalData)
		{
			var accountEntity = _accountDbConverter.ToEntity(personalData.SessionContext.Account);

			if (personalData.UserName != null)
			{
				accountEntity.UserName = personalData.UserName;
			}

			if (personalData.Email != null)
			{
				accountEntity.Email = personalData.Email;
			}

			var identityResult = await _userManager.UpdateAsync(accountEntity);

			if (identityResult.Succeeded)
			{
				return ServiceResponse.Success(IdentityErrorCode.Success);
			}

			var errorCode = identityResult.FirstErrorOrDefault();

			return ServiceResponse.Failure(errorCode);
		}

		public async Task<ServiceResponse<IdentityErrorCode>> UpdatePassword(UpdatePassword updatePassword)
		{
			var accountEntity = _accountDbConverter.ToEntity(updatePassword.SessionContext.Account);

			var result = await _userManager.ChangePasswordAsync(
				accountEntity,
				updatePassword.OldPassword,
				updatePassword.NewPassword);

			if (result.Succeeded)
			{
				return ServiceResponse.Success(IdentityErrorCode.Success);
			}

			var errorCode = result.FirstErrorOrDefault();

			return ServiceResponse.Failure(errorCode);
		}
	}
}