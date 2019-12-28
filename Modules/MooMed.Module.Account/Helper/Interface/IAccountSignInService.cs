﻿using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;
using MooMed.Core.DataTypes;

namespace MooMed.Module.Accounts.Helper.Interface
{
    public interface IAccountSignInService
    {
        [NotNull]
        Task<RegistrationResult> Register([NotNull] RegisterModel registerModel);

        [NotNull]
        Task<WorkerResponse<LoginResult>> Login([NotNull] LoginModel loginModel);

        [NotNull]
        Task<bool> RefreshLastAccessed([NotNull] ISessionContext sessionContext);
    }
}
