using Microsoft.AspNetCore.Identity;
using MooMed.AspNetCore.Identity.Extension;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.Definitions.Models.User.ErrorCodes;
using MooMed.Common.ServiceBase;
using MooMed.Core.DataTypes;
using MooMed.Eventing.Events.MassTransit.Interface;
using MooMed.Logging.Abstractions.Interface;
using MooMed.Module.Accounts.Converters;
using MooMed.Module.Accounts.Datatypes.Entity;
using MooMed.Module.Accounts.Repository.Converters;
using MooMed.Module.Accounts.Service.Interface;
using System.Threading.Tasks;
using MooMed.Common.Definitions.Notifications;
using MooMed.DotNet.Extensions;
using MooMed.DotNet.Utils.Tasks;
using MooMed.Eventing.Helper;
using MooMed.Module.Accounts.Datatypes.SignalR;

namespace MooMed.Module.Accounts.Service
{
	internal class PersonalDataService : MooMedServiceBase, IPersonalDataService
	{
		private readonly AccountDbConverter _accountDbConverter;

		private readonly AccountModelToUiModelConverter _accountModelToUiModelConverter;

		private readonly UserManager<AccountEntity> _userManager;

		private readonly IFriendsService _friendsService;

		private readonly IMassTransitSignalRBackplaneService _signalRBackplaneService;

		public PersonalDataService(
			IMooMedLogger logger,
			AccountDbConverter accountDbConverter,
			AccountModelToUiModelConverter accountModelToUiModelConverter,
			UserManager<AccountEntity> userManager,
			IFriendsService friendsService,
			IMassTransitSignalRBackplaneService signalRBackplaneService)
			: base(logger)
		{
			_accountDbConverter = accountDbConverter;
			_accountModelToUiModelConverter = accountModelToUiModelConverter;
			_userManager = userManager;
			_friendsService = friendsService;
			_signalRBackplaneService = signalRBackplaneService;
		}

		public async Task<ServiceResponse<IdentityErrorCode>> UpdatePersonalData(PersonalData personalDataModel)
		{
			var accountEntity = await _userManager.FindByEmailAsync(personalDataModel.Email);

			if (personalDataModel.UserName != null)
			{
				accountEntity.UserName = personalDataModel.UserName;
			}

			if (personalDataModel.Email != null)
			{
				accountEntity.Email = personalDataModel.Email;
			}

			var identityResult = await _userManager.UpdateAsync(accountEntity);

			if (identityResult.Succeeded)
			{
				FireAndForgetTask.Run(async () =>
				{
					var friendsTask = _friendsService.GetFriends(personalDataModel.SessionContext);

					var createNotification = FrontendNotificationFactory.Update(new AccountChangeNotification()
					{
						Account = _accountModelToUiModelConverter.ToUiModel(personalDataModel.SessionContext.Account)
					}, NotificationType.AccountChange);

					await (await friendsTask).ParallelAsync(friend => _signalRBackplaneService.RaiseGroupSignalREvent(friend.Id.ToString(), createNotification));
				}, Logger);

				return ServiceResponse.Success(IdentityErrorCode.Success);
			}

			var errorCode = identityResult.FirstErrorOrDefault();

			return ServiceResponse.Failure(errorCode);
		}

		public async Task<ServiceResponse<IdentityErrorCode>> UpdatePassword(UpdatePassword updatePasswordModel)
		{
			var accountEntity = _accountDbConverter.ToEntity(updatePasswordModel.SessionContext.Account);

			var result = await _userManager.ChangePasswordAsync(
				accountEntity,
				updatePasswordModel.OldPassword,
				updatePasswordModel.NewPassword);

			if (result.Succeeded)
			{
				return ServiceResponse.Success(IdentityErrorCode.Success);
			}

			var errorCode = result.FirstErrorOrDefault();

			return ServiceResponse.Failure(errorCode);
		}
	}
}