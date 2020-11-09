﻿using System;
using System.Collections.Generic;
using MooMed.Common.Definitions.Models.Finance;
using MooMed.Common.Definitions.Models.Saving;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.Definitions.Models.User.ErrorCodes;
using MooMed.Common.Definitions.Notifications;
using MooMed.Module.Saving.DataTypes;

namespace FrontendEnumGenerator
{
	public static class EnumsToExport
	{
		public static readonly List<Type> Enums = new List<Type>()
		{
			typeof(LoginResponseCode),
			typeof(AccountValidationResult),
			typeof(AccountOnlineState),
			typeof(NotificationType),
			typeof(ExchangeTradedType),
			typeof(IdentityErrorCode),
			typeof(Currency)
		};
	}
}