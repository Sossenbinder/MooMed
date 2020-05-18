﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;
using MooMed.Core.DataTypes;

namespace MooMed.Module.Accounts.Service.Interface
{
	public interface ILoginService
	{
		[NotNull]
		Task<ServiceResponse<LoginResult>> Login([NotNull] LoginModel loginModel);

		[NotNull]
		Task<bool> RefreshLastAccessed([NotNull] ISessionContext sessionContext);
	}
}
