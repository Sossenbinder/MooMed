using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MooMed.AspNetCore.Identity.Extension;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.Definitions.Models.User.ErrorCodes;
using MooMed.Common.ServiceBase;
using MooMed.Core.DataTypes;
using MooMed.DotNet.Utils.Tasks;
using MooMed.Module.Accounts.Datatypes.Entity;
using MooMed.Module.Accounts.Repository.Converters;
using MooMed.Module.Accounts.Service.Interface;

namespace MooMed.Module.Accounts.Service
{
	internal class PersonalDataService : MooMedServiceBase, IPersonalDataService
	{
		private readonly AccountDbConverter _accountDbConverter;

		private readonly UserManager<AccountEntity> _userManager;

		public PersonalDataService(
			AccountDbConverter accountDbConverter,
			UserManager<AccountEntity> userManager)
		{
			_accountDbConverter = accountDbConverter;
			_userManager = userManager;
		}

		public async Task<ServiceResponse<IdentityErrorCode>> UpdatePersonalData(PersonalData personalData)
		{
			var accountEntity = await _userManager.FindByEmailAsync(personalData.Email);

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
				FireAndForgetTask.Run(async () =>
				{
				}, )

				return ServiceResponse.Success(IdentityErrorCode.Success);
			}

			var errorCode = result.FirstErrorOrDefault();

			return ServiceResponse.Failure(errorCode);
		}
	}
}