﻿using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.Definitions.Models.User.ErrorCodes;
using MooMed.Core.DataTypes;
using MooMed.ServiceBase.Definitions.Interface;

namespace MooMed.ServiceBase.Services.Interface
{
	[ServiceContract]
	public interface IAccountService : IGrpcService
	{
		[OperationContract]
		[NotNull]
		Task<ServiceResponse<LoginResult>> Login([NotNull] LoginModel loginModel);

		[OperationContract]
		[NotNull]
		Task RefreshLoginForAccount([NotNull] Primitive<int> accountId);

		[OperationContract]
		[NotNull]
		Task<ServiceResponse<RegistrationResult>> Register([NotNull] RegisterModel registerModel);

		[OperationContract]
		[NotNull]
		Task<ServiceResponse> LogOff([NotNull] ISessionContext sessionContext);

		[OperationContract]
		[ItemNotNull]
		Task<ServiceResponse<Account>> FindById([NotNull] Primitive<int> accountId);

		[OperationContract]
		[NotNull]
		Task<ServiceResponse<List<Account>>> FindAccountsStartingWithName([NotNull] string name);

		[OperationContract]
		[CanBeNull]
		Task<ServiceResponse<Account>> FindByEmail([NotNull] string email);

		[OperationContract]
		Task<ServiceResponse> AddAsFriend([NotNull] AddAsFriendModel model);

		[OperationContract]
		Task<ServiceResponse<List<Friend>>> GetFriends([NotNull] ISessionContext sessionContext);

		[OperationContract]
		Task<ServiceResponse<IdentityErrorCode>> UpdatePersonalData(PersonalData personalData);

		[OperationContract]
		Task<ServiceResponse<IdentityErrorCode>> UpdatePassword(UpdatePassword updatePasswordData);
	}
}